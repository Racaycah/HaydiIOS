using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XFGloss;
using Xamarin.Forms;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using System.Globalization;
using Xamarin.Forms.Maps;

namespace HaydiIOS
{
    public partial class ActivityDetailsPage : ContentPage
    {
        Activity test = new Activity();
        bool select = new bool();
        DateTime date = new DateTime();
        private string amount;
        private string type;
        CalendarEvent newEvent = new CalendarEvent();

        public ActivityDetailsPage(Activity activity)
        {
            InitializeComponent();

            TypePicker.Items.Add("saat");
            TypePicker.Items.Add("gün");
            TypePicker.Items.Add("hafta");

            TypePicker.SelectedIndexChanged += TypePicker_SelectedIndexChanged;

            TypePicker.SelectedIndex = 0;

            test = activity;
            GuestsView.ItemsSource = activity.guests;

            var newDate = activity.date.Replace(" ", ".") + " " + activity.time.Replace(" ", "");

            var list = activity.date.Split(' ');
            var time = activity.time.Replace(" ", "").Split(':');

            string DateFormat = "dd." + (list.ElementAt(1).Length == 1 ? "M" : "MM") + ".yyyy HH:" + (time.ElementAt(1).Length == 1 ? "m" : "mm");

            date = DateTime.ParseExact(newDate, DateFormat, CultureInfo.InvariantCulture);

            string author = FindAuthor(activity.author_id);

            AuthorLabel.Text = "Düzenleyen : " + author;
            EventNameLabel.Text = activity.name;
            DateLabel.Text = "Tarih : " + newDate.Remove(10);
            TimeLabel.Text = activity.time;

            GuestsView.ItemSelected += GuestsView_ItemSelected;
            AddReminderButton.Clicked += AddReminderClicked;

            AmountPicker.SelectedIndex = 0;

            AmountPicker.SelectedIndexChanged += AmountPicker_SelectedIndexChanged;

            //GuestsView.ItemSelected += GuestSelected;

            //              < xfg:CellGloss.BackgroundGradient > 
            //     < xfg:Gradient StartColor = "Red" EndColor = "Maroon" IsRotationTopToBottom = "true" />      
            //        </ xfg:CellGloss.BackgroundGradient >
        }

        private void AmountPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(AmountPicker.SelectedIndex > -1)
            amount = AmountPicker.Items.ElementAt(AmountPicker.SelectedIndex);
        }

        private async void AddReminderClicked(object sender, EventArgs e)
        {
            if((AmountPicker.Items.ElementAt(AmountPicker.SelectedIndex) == null) || (TypePicker.Items.ElementAt(TypePicker.SelectedIndex) == null)){
                await DisplayAlert("Hata!", "Lütfen bütün alanların seçili olduğundan emin olunuz.", "Tamam");
            }
            else
            {
                var selection = await DisplayAlert("Onay", "Bu etkinlikten " + AmountPicker.Items.ElementAt(AmountPicker.SelectedIndex) + " " + TypePicker.Items.ElementAt(TypePicker.SelectedIndex) + " önce haber almak istiyor musunuz?", "Evet", "Hayır");

                if (selection)
                {
                    var reminder = new CalendarEventReminder();
                    reminder.Method = CalendarReminderMethod.Alert;

                    switch (type)
                    {
                        case "saat":
                            reminder.TimeBefore = TimeSpan.FromHours(Convert.ToDouble(amount));
                            break;
                        case "gün":
                            reminder.TimeBefore = TimeSpan.FromDays(Convert.ToDouble(amount));
                            break;
                        case "hafta":
                            reminder.TimeBefore = TimeSpan.FromDays((Convert.ToDouble(amount)) * 7);
                            break;
                    }

                    var calendars = await Data.CurrentCalendar.GetCalendarsAsync();
                    var date = test.date + " " + test.time + ":00";
                    var datetime = DateTime.ParseExact(date, "dd MM yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    foreach(var calendar in calendars)
                    {
                        var events = await Data.CurrentCalendar.GetEventsAsync(calendar, datetime, datetime.AddDays(1));
                        newEvent = events.Where(i => i.Name == test.name && i.Start == datetime).FirstOrDefault();

                        if(newEvent != null)
                        {
                            await Data.CurrentCalendar.AddEventReminderAsync(newEvent, reminder);

                            await DisplayAlert("Tamam", "Hatırlatıcı başarıyla eklendi.", "Tamam");

                        }
                    }
                }
            }
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmountPicker.Items.Clear();
            var picker = sender as Picker;
            var typeString = picker.Items.ElementAt(picker.SelectedIndex);
            type = typeString;

            switch (typeString)
            {
                case "saat":
                    for(int i = 1; i < 25; i++)
                    {
                        AmountPicker.Items.Add(i.ToString());
                    }
                    break;
                case "gün":
                    for(int i = 1; i < 8; i++)
                    {
                        AmountPicker.Items.Add(i.ToString());
                    }
                    break;
                case "hafta":
                    for(int i = 1; i < 5; i++)
                    {
                        AmountPicker.Items.Add(i.ToString());
                    }
                    break;
            }
            AmountPicker.SelectedIndex = 0;
        }

        private void GuestsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            else
            {
                GuestsView.SelectedItem = null;
            }
        }

        public async void AcceptEvent(object sender, EventArgs e)
        {
            select = await DisplayAlert("Gidiyorum !", "Etkinliği kabul etmek istiyor musunuz ?", "Evet", "Hayır");

            if (select)
            {
                using (var client = new HttpClient())
                {
                    var obj = new AnswerActivity
                    {
                        activity_id = test.id.ToString(),
                        status = "1",
                        user_id = Data.UserId.ToString()
                    };
                    var json = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/answerActivity", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonobj = JsonConvert.DeserializeObject<ServerResponse>(result);
                        if (jsonobj.status == "0")
                        {
                            await DisplayAlert("Tamam!", "Arkadaşlarınıza " + test.name + " için katılacağınızı söylediniz!", "Tamam");
                            Data.EventEdited = true;

                            var coder = new Geocoder();
                            var lat = test.location.Split('/').ElementAt(0);
                            var longit = test.location.Split('/').ElementAt(1);

                            double latitude = Double.Parse(lat, CultureInfo.InvariantCulture);
                            double longitude = Double.Parse(longit, CultureInfo.InvariantCulture);

                            var loc = await coder.GetAddressesForPositionAsync(new Position(latitude, longitude));

                            var locat = loc.ElementAt(0).Replace('/', ' ');

                            var CurrentCalendar = CrossCalendars.Current;
                            var calendars = await CurrentCalendar.GetCalendarsAsync();

                            var newEvent = new CalendarEvent();
                            newEvent.Name = test.name + " " + FindAuthor(test.author_id) + " tarafından düzenlenmiş.";
                            newEvent.Location = loc.ElementAt(0);
                            newEvent.Start = date;
                            newEvent.End = newEvent.Start;
                            newEvent.ExternalID = test.id.ToString();

                            foreach (var calendar in calendars)
                            {
                                await CurrentCalendar.AddOrUpdateEventAsync(calendar, newEvent);
                            }

                            await DisplayAlert("Tamam", "Etkinlik, takviminize kaydedildi.", "Tamam");
                        }
                    }
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (Data.EventEdited)
            {
                Data.RefreshEvents();
                Data.EventEdited = false;
            }
        }

        public async void RejectEvent(object sender, EventArgs e)
        {
            select = await DisplayAlert("Hayatta Gitmem !", "Etkinliği reddetmek istediğinize emin misiniz ?\nSonra arkadaşlarınız sizsiz eğlenirler, pişman olmayın.", "Evet", "Hayır");

            if (select)
            {
                using (var client = new HttpClient())
                {
                    var obj = new AnswerActivity
                    {
                        activity_id = test.id.ToString(),
                        status = "2",
                        user_id = Data.UserId.ToString()
                    };
                    var json = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/answerActivity", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonobj = JsonConvert.DeserializeObject<ServerResponse>(result);
                        if (jsonobj.status == "0")
                        {
                            await DisplayAlert("Tamam!", "Arkadaşlarınıza " + test.name + " için katılmayacağınızı söylediniz.", "Tamam");

                            //var CurrentCalendar = CrossCalendars.Current;
                            //var calendars = await CurrentCalendar.GetCalendarsAsync();


                            //Bütün eventleri alıp incele.

                            //foreach(var calendar in calendars)
                            //{
                            //    var events = await CurrentCalendar.GetEventsAsync(calendar, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
                            //    var deleteEvent = await CurrentCalendar.GetEventByIdAsync(calendar.ExternalID + test.id.ToString());

                            //    if(deleteEvent != null)
                            //    {
                            //        await CurrentCalendar.DeleteEventAsync(calendar, deleteEvent);
                            //        await DisplayAlert("Tamam", "Etkinlik takviminizden silindi","Tamam");
                            //    }
                            //    else
                            //    {
                            //        await DisplayAlert("", "Etkinlik takviminize kayıtlı değilmiş.", "Tamam");
                            //    }
                            //}

                            //var deleteEvent = await CurrentCalendar.GetEventByIdAsync(test.id.ToString());

                            //if (deleteEvent != null)
                            //{
                            //    foreach (var calendar in calendars)
                            //    {
                            //        await CurrentCalendar.DeleteEventAsync(calendar, deleteEvent);
                            //    }
                            //    await DisplayAlert("Tamam", "Etkinlik, takviminizden silindi.", "Tamam");
                            //}

                            Data.EventEdited = true;
                            Data.Activities.Remove(Data.Activities.Where(i => i.id == test.id).FirstOrDefault());
                            //Data.Activities.Clear();
                            //Data.GetUserEvents(Data.UserId.ToString());
                            await Navigation.PopAsync();
                        }
                    }
                }
            }
        }

        private string FindAuthor(int id)
        {
            foreach (Guest guest in test.guests)
            {
                if (id == guest.id)
                {
                    return guest.name;
                }
            }
            return null;
        }

        public async void SeeLocation(object sender, EventArgs e)
        {
            string coords = test.location;
            await Navigation.PushAsync(new MapPage(coords));
        }
    }
}
