using Newtonsoft.Json;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HaydiIOS
{
    public partial class CreateEventPage : ContentPage
    {
        Geocoder geocoder = new Geocoder();
        public CreateEventPage()
        {
            InitializeComponent();

            Data.GroupsList.CollectionChanged += GroupsChanged;

            LoadingIndicator.IsVisible = false;

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += UseCurrent;

            UseCurrentLocation.Source = "current_position_icon.png";
            UseCurrentLocation.Aspect = Aspect.AspectFit;
            UseCurrentLocation.GestureRecognizers.Add(tgr);

            var deleteTgr = new TapGestureRecognizer();
            deleteTgr.Tapped += ResetText;

            CategoryImage.Source = ImageSource.FromFile("thinking_man.png");
            CategoryImage.Aspect = Aspect.AspectFit;
            CalendarImage.Source = ImageSource.FromFile("calendar_icon.png");
            CalendarImage.Aspect = Aspect.AspectFit;
            ClockImage.Source = ImageSource.FromFile("clock_icon.png");
            ClockImage.Aspect = Aspect.AspectFit;
            EditIcon.Source = ImageSource.FromFile("edit_pen.png");
            EditIcon.Aspect = Aspect.AspectFit;

            EditIcon.GestureRecognizers.Add(deleteTgr);

            AddressEntry.Completed += GeocodeAddress;

            CategoryPicker.Items.Add("Futbol");
            CategoryPicker.Items.Add("Gezi");
            CategoryPicker.Items.Add("Yemek");
            CategoryPicker.Items.Add("Sohbet");
            CategoryPicker.Items.Add("Çay");
            CategoryPicker.Items.Add("Toplantı");
            CategoryPicker.Items.Add("Sinema");
            DatePicker.MinimumDate = DateTime.Today;
            TimePicker.Format = "HH:mm";

            CategoryPicker.SelectedIndexChanged += ChangeCategoryIcon;

            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(Data.CurrentLatitude, Data.CurrentLongitude), Distance.FromKilometers(0.5)));

            foreach (CustomContact cc in Data.PeopleList)
            {
                PeoplePicker.Items.Add(cc.Name);
            }
        }

        private void ResetText(object sender, EventArgs e)
        {
            AddressEntry.Text = "";
        }

        private void ChangeCategoryIcon(object sender, EventArgs e)
        {
            switch (CategoryPicker.SelectedIndex)
            {
                case 0:
                    CategoryImage.Source = ImageSource.FromFile("football.jpg");
                    break;
                case 1:
                    CategoryImage.Source = ImageSource.FromFile("choose_activity_icon.jpg");
                    break;
                case 2:
                    CategoryImage.Source = ImageSource.FromFile("food.png");
                    break;
                case 3:
                    CategoryImage.Source = ImageSource.FromFile("conversation.png");
                    break;
                case 4:
                    CategoryImage.Source = ImageSource.FromFile("tea.png");
                    break;
                case 5:
                    CategoryImage.Source = ImageSource.FromFile("meeting.png");
                    break;
                case 6:
                    CategoryImage.Source = ImageSource.FromFile("cinema.png");
                    break;
                default:
                    CategoryImage.Source = ImageSource.FromFile("thinking_man.png");
                    break;
            }
        }

        private void UseCurrent(object sender, EventArgs e)
        {
            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(Data.CurrentLatitude, Data.CurrentLongitude), Distance.FromKilometers(0.5)));
        }

        private async void GeocodeAddress(object sender, EventArgs e)
        {
            var address = AddressEntry.Text;
            if (address != null || address != "")
            {
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;

                var approxLocations = await geocoder.GetPositionsForAddressAsync(address);

                Position approxPos = new Position(approxLocations.ElementAt(0).Latitude, approxLocations.ElementAt(0).Longitude);

                if (LocationMap.Pins.Any())
                {
                    LocationMap.Pins.Clear();
                }

                var customPin = new Pin();
                customPin.Type = PinType.SearchResult;
                customPin.Position = approxPos;
                customPin.Label = address;

                LocationMap.Pins.Add(customPin);

                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;

                LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(approxPos, Distance.FromKilometers(0.5)));
            }
        }

        public List<string> FindGuestPhones(string name)
        {
            var group = Data.FindGroup(name);
            List<string> str = new List<string>();

            foreach (CustomContact cc in group.People)
            {
                if (cc.Number != null)
                {
                    var tmp = cc.Number;
                    tmp = Regex.Replace(cc.Number, @"\s+", "");
                    tmp = tmp.Replace("(", "");
                    tmp = tmp.Replace(")", "");
                    str.Add(tmp);
                }
            }
            return str;
        }

        public void GroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PeoplePicker.Items.Clear();

            foreach (CustomGroup group in Data.GroupsList)
            {
                PeoplePicker.Items.Add(group.Name);
            }
            //PeoplePicker.Items.Add(Data.GroupsList.ElementAt(Data.GroupsList.Count - 1).Name);
        }

        public async void CreateEvent(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Hata!", "Lütfen bir kategori seçiniz.", "Tamam");
            }
            else if (PeoplePicker.SelectedIndex < 0)
            {
                await DisplayAlert("Hata!", "Lütfen davetli seçiniz.", "Tamam");
            }
            else
            {
                var author = Data.UserId;
                var location = LocationMap.VisibleRegion.Center.Latitude.ToString().Replace(",", ".") + "/" + LocationMap.VisibleRegion.Center.Longitude.ToString().Replace(",", ".");
                var dateWithDot = DatePicker.Date.ToString("dd.MM.yyyy");
                var name = CategoryPicker.Items.ElementAt(CategoryPicker.SelectedIndex);
                var time = TimePicker.Time.ToString().Remove(5);
                var date = dateWithDot.Replace(".", " ");
                List<int> guests = new List<int>();

                List<string> phones = FindGuestPhones(PeoplePicker.Items[PeoplePicker.SelectedIndex]);
                phones.Add(Data.PhoneNumber);

                var client = new HttpClient();

                var obj = new FilterObject()
                {
                    phones = phones
                };

                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/filterUsers", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonobj = JsonConvert.DeserializeObject<FilterResponse>(result);

                    if (jsonobj.data.users.Any()) //&& jsonobj.data.users.Count > 1)
                    {
                        foreach (User user in jsonobj.data.users)
                        {
                            guests.Add(user.id);
                        }

                        var actObj = new CreateActivity
                        {
                            author_id = author,
                            date = date,
                            guests = guests,
                            location = location,
                            name = name,
                            time = time
                        };

                        var actJson = JsonConvert.SerializeObject(actObj);
                        var actContent = new StringContent(actJson, Encoding.UTF8, "application/json");

                        HttpResponseMessage actResponse = await client.PostAsync("http://haydi.naezith.com:2095/submitActivity", actContent);

                        if (actResponse.IsSuccessStatusCode)
                        {
                            var actResult = await actResponse.Content.ReadAsStringAsync();
                            var submit = JsonConvert.DeserializeObject<SubmitActivityObject>(actResult);

                            if (submit.status == "0")
                            {
                                await DisplayAlert("Tamam", "Etkinlik oluşturuldu!\nEtkinlik numarası : " + submit.data.activity_id.ToString(), "Tamam");

                                var ansObj = new AnswerActivity
                                {
                                    activity_id = submit.data.activity_id.ToString(),
                                    status = "1",
                                    user_id = Data.UserId.ToString()
                                };
                                var ansJson = JsonConvert.SerializeObject(ansObj);
                                var ansContent = new StringContent(ansJson, Encoding.UTF8, "application/json");

                                HttpResponseMessage ansResponse = await client.PostAsync("http://haydi.naezith.com:2095/answerActivity", ansContent);

                                if (ansResponse.IsSuccessStatusCode)
                                {
                                    var ansResult = await ansResponse.Content.ReadAsStringAsync();
                                    var acceptObj = JsonConvert.DeserializeObject<ServerResponse>(ansResult);
                                    if (acceptObj.status == "0")
                                    {                                        
                                        Data.EventEdited = true;
                                    }
                                }

                                await DisplayAlert("Test", "Etkinlik onaylandı", "Tamam");

                                Data.RefreshEvents();

                                try
                                {
                                    var calendars = await Data.CurrentCalendar.GetCalendarsAsync();

                                    var str = dateWithDot + " " + time;

                                    var lat = actObj.location.Split('/').ElementAt(0);
                                    var longit = actObj.location.Split('/').ElementAt(1);

                                    double latitude = Double.Parse(lat, CultureInfo.InvariantCulture);
                                    double longitude = Double.Parse(longit, CultureInfo.InvariantCulture);

                                    await DisplayAlert("Test2", "Takvimler alındı", "Tamam");

                                    var locs = await geocoder.GetAddressesForPositionAsync(new Position(latitude, longitude));

                                    var newEvent = new CalendarEvent();
                                    newEvent.Name = actObj.name;
                                    newEvent.Location = actObj.location.Replace('/', ' ');
                                    newEvent.Start = DateTime.ParseExact(str, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                                    newEvent.End = newEvent.Start;
                                    newEvent.ExternalID = submit.data.activity_id.ToString();
                                    newEvent.Description = locs.ElementAt(0) + "'de yapılacaktır.";

                                    await DisplayAlert("Takvim", "Takvime kaydedilecek", "Tamam");

                                    foreach (var calendar in calendars)
                                    {
                                        await Data.CurrentCalendar.AddOrUpdateEventAsync(calendar, newEvent);
                                    }

                                    await DisplayAlert("Tamam", "Etkinlik, takvimlerinize kaydedildi.", "Tamam");

                                    var addReminder = await DisplayAlert("Hatırlatma?", "Etkinlik için bir hatırlatıcı oluşturmak ister misiniz?\n(İsterseniz daha sonra etkinlik penceresinden de ekleyebilirsiniz.)", "Evet", "Hayır");

                                    bool flag = true;

                                    while (flag)
                                    {
                                        if (addReminder)
                                        {
                                            var reminder = new CalendarEventReminder();
                                            reminder.Method = CalendarReminderMethod.Alert;

                                            var selection = await DisplayActionSheet("Süre Seçiniz", "İptal", null, "1 gün", "1 hafta");

                                            switch (selection)
                                            {
                                                case "1 gün":
                                                    reminder.TimeBefore = TimeSpan.FromDays(1);
                                                    break;
                                                case "1 hafta":
                                                    reminder.TimeBefore = TimeSpan.FromDays(7);
                                                    break;
                                                default:
                                                    flag = false;
                                                    continue;
                                            }

                                            foreach (var calendar in calendars)
                                            {
                                                var events = await Data.CurrentCalendar.GetEventsAsync(calendar, newEvent.Start, newEvent.Start.AddDays(1));
                                                var testEvent = events.Where(i => i.Name == newEvent.Name && i.Start == newEvent.Start).FirstOrDefault();

                                                if (testEvent != null)
                                                {
                                                    await Data.CurrentCalendar.AddEventReminderAsync(testEvent, reminder);

                                                    await DisplayAlert("Tamam", "Hatırlatıcı başarıyla eklendi.", "Tamam");
                                                }
                                                else
                                                {
                                                    await DisplayAlert("Hata", "Etkinlik takvimde bulunamadı.", "Tamam");
                                                }
                                            }
                                        }
                                        else break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    await DisplayAlert("Hata", "Bir hata oluştu.", "Tamam");
                                    throw ex;
                                }
                            }
                            else
                            {
                                await DisplayAlert("Hata", "Etkinlik oluşturulamadı. Lütfen daha sonra tekrar deneyiniz.", "Tamam");
                            }
                        }
                    }
                    else
                    {
                        var selection = await DisplayAlert("Hata!", "Gruptakiler uygulamaya kayıtlı değil :(\nHaydi onları da Haydi'ye davet et!", "Tamam", "Hayır");

                        if (selection)
                        {
                            var action = await DisplayActionSheet("Haydi Gel Benimle Ol!", "Hayır", null, "Ayrı Ayrı Davet Et", "Tümünü Davet Et");

                            //TODO: Etkinliğe davet etme sayfası.
                            switch (action)
                            {
                                case ("Ayrı Ayrı Davet Et"):
                                    {
                                        break;
                                    }
                                case ("Tümünü Davet Et"):
                                    {
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Hata!", "Sunucuya bağlantıda bir hata oldu.", "Tamam");
                }
            }
        }
    }
}
