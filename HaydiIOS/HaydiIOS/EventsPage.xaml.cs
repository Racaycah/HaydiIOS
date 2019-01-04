using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class EventsPage : ContentPage
    {

        public EventsPage()
        {
            InitializeComponent();

            EventsView.ItemsSource = Data.Activities;
            
            Data.GetUserEvents(Data.UserId.ToString());
        }

        //public async void RefreshEvents(object sender, EventArgs e)
        //{
        //    Data.Activities.Clear();
        //    Data.GetUserEvents(Data.UserId.ToString());
        //}

        //public async void GetUserEvents(string UserID )
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var obj = new ActivityObject()
        //        {
        //            user_id = UserID
        //        };
        //        var json = JsonConvert.SerializeObject(obj);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        try
        //        {
        //            HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/getActivities", content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var result = await response.Content.ReadAsStringAsync();
        //                var jsonobj = JsonConvert.DeserializeObject<ActivityResponse>(result);
        //                foreach (Activity a in jsonobj.data.activities)
        //                {
        //                    Data.Activities.Add(a);
        //                }
        //            }
        //        }
        //        catch (TaskCanceledException ex)
        //        {
        //            var cancel = ex.CancellationToken.IsCancellationRequested ? "cancel" : "timeout";
        //        }
        //    }            
        //}

        public async void EventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(((ListView)sender).SelectedItem == null)
            {
                return;
            }
            var item = e.SelectedItem as Activity;
            await Navigation.PushAsync(new ActivityDetailsPage(item));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
