using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaydiIOS
{
    public class LoginObject
    {
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class LoginData
    {
        public int id { get; set; }
    }

    public class LoginResponse
    {
        public string status { get; set; }
        public LoginData data { get; set; }
    }
}
