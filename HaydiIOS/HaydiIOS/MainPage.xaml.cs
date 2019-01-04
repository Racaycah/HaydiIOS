using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using System.Net.Http;
using Newtonsoft.Json;
namespace HaydiIOS
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //public async void Login()
        //{

        //    using(var client = new HttpClient())
        //    {
        //        var obj = new LoginObject()
        //        {
        //            name = "Ata Doruk",
        //            phone = "05559998833"
        //        };
        //        var json = JsonConvert.SerializeObject(obj);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/login", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            var jsonobj = JsonConvert.DeserializeObject<LoginResponse>(result);
        //            Data.UserId = jsonobj.data.id;
        //        }
        //    }        
    }
}
