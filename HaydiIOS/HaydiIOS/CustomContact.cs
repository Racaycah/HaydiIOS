using Newtonsoft.Json;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HaydiIOS
{
    public class CustomContact
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(CustomGroup))]
        public int GroupKey { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }

        public CustomContact()
        {
        }
    }

    public class CustomGroup : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [OneToMany("GroupKey")]
        public List<CustomContact> People { get; set; }

        public string Name { get; set; }
        public bool IsFavourite { get; set; }

        public CustomGroup()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class FilterObject
    {
        public List<string> phones { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class Users
    {
        public List<User> users { get; set; }
    }

    public class FilterResponse
    {
        public string status { get; set; }
        public Users data { get; set; }
    }

    public static class Data
    {
        public static ObservableCollection<CustomGroup> GroupsList = new ObservableCollection<CustomGroup>();
        public static ObservableCollection<CustomContact> PeopleList = new ObservableCollection<CustomContact>();
        public static ObservableCollection<CustomContact> OtherPeople = new ObservableCollection<CustomContact>();
        public static ObservableCollection<Activity> Activities = new ObservableCollection<Activity>();
        public static string PhoneNumber = "05376692694";
        public static string DBPath;
        public static GroupsDB db = new GroupsDB();
        public static bool AppLoading = new bool();
        public static bool EventEdited = new bool();
        public static bool GroupEdited = new bool();
        public static ICalendars CurrentCalendar = CrossCalendars.Current;

        public static async void RefreshEvents()
        {
            Data.Activities.Clear();
            Data.GetUserEvents(Data.UserId.ToString());
        }

        public static async void GetUserEvents(string UserID)
        {
            using (var client = new HttpClient())
            {
                var obj = new ActivityObject()
                {
                    user_id = UserID
                };
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/getActivities", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonobj = JsonConvert.DeserializeObject<ActivityResponse>(result);
                        foreach (Activity a in jsonobj.data.activities)
                        {
                            Data.Activities.Add(a);
                        }
                    }
                }
                catch (TaskCanceledException ex)
                {
                    var cancel = ex.CancellationToken.IsCancellationRequested ? "cancel" : "timeout";
                }
            }
        }


        public static CustomGroup FindGroup(string GroupName)
        {
            foreach (CustomGroup cg in Data.GroupsList)
            {
                if (cg.Name == GroupName)
                    return cg;
            }
            return null;
        }

        public static double CurrentLatitude = new double();
        public static double CurrentLongitude = new double();

        public static int UserId
        {
            get; set;
        }
        public static string BaseURL = "http://haydi.naezith.com:2095/";
    }

    public class SubmitActivityData
    {
        public int activity_id { get; set; }
    }

    public class SubmitActivityObject
    {
        public string status { get; set; }
        public SubmitActivityData data { get; set; }
    }
}
