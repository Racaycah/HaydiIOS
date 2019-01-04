using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            Loading.IsVisible = false;

            BackgroundColor = Color.MediumTurquoise;
        }

        public async void Login(object sender, EventArgs e)
        {
            if (Username.Text == null || Phone.Text == null)
            {
                await DisplayAlert("Hata", "Lütfen bütün alanları doldurunuz", "Tamam");
            }
            else
            {
                Loading.IsRunning = true;
                Loading.IsVisible = true;
                
                using (var client = new HttpClient())
                {
                    var obj = new LoginObject()
                    {
                        //name = "Ata Doruk",
                        //phone = "05376692694"
                        name = Username.Text,
                        phone = Phone.Text
                    };
                    var json = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("http://haydi.naezith.com:2095/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonobj = JsonConvert.DeserializeObject<LoginResponse>(result);
                        Data.UserId = jsonobj.data.id;
                    }
                }

                var locator = CrossGeolocator.Current;
                locator.AllowsBackgroundUpdates = true;
                locator.DesiredAccuracy = 50;

                try
                {
                    var position = await locator.GetPositionAsync(10000);

                    if (position == null)
                    {
                        await DisplayAlert("Hata", "Konum bilgisi alınamadı. Lütfen konum ayarlarınızın açık olduğundan emin olunuz.", "Tamam");
                    }
                    else
                    {
                        Data.CurrentLatitude = position.Latitude;
                        Data.CurrentLongitude = position.Longitude;
                        Loading.IsRunning = false;
                        Loading.IsVisible = false;
                        await Navigation.PushModalAsync(new MainPage());
                    }
                }
                catch (Exception ex) //Plugin.Geolocator.Abstractions.GeolocationException ex
                {
                    throw ex;
                }
            }
        }
    }
}
