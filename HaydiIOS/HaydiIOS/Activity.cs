using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaydiIOS
{
    public class Guest
    {
        public int id { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public int status { get; set; }
    }

    public class ActivityObject
    {
        public string user_id { get; set; }
    }

    public class Activity
    {
        public int id { get; set; }
        public int author_id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string location { get; set; }
        public List<Guest> guests { get; set; }
    }

    public class ActivityData
    {
        public List<Activity> activities { get; set; }
    }

    public class ActivityResponse
    {
        public string status { get; set; }
        public ActivityData data { get; set; }
    }

    public class CreateActivity
    {
        public int author_id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string location { get; set; }
        public List<int> guests { get; set; }
    }

    public class AnswerActivity
    {
        public string user_id { get; set; }
        public string activity_id { get; set; }
        public string status { get; set; }
    }

    public class ServerResponse
    {
        public string status { get; set; }
    }

}
