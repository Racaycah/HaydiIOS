using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Globalization;

namespace HaydiIOS
{
    public partial class MapPage : ContentPage
    {
        public MapPage(string coords)
        {
            InitializeComponent();

            coords = coords.Replace(",", ".");

            string lat = coords.Split('/').ElementAt(0);
            string longit = coords.Split('/').ElementAt(1);

            double latitude = Double.Parse(lat,CultureInfo.InvariantCulture);
            double longitude = Double.Parse(longit,CultureInfo.InvariantCulture);

            LocationMap.Pins.Add(new Pin {
                Position=new Xamarin.Forms.Maps.Position(latitude,longitude),
                Label="Etkinlik Yeri",
                Type=PinType.Generic
            });

            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(latitude, longitude), Distance.FromKilometers(0.5)));
        }
    }
}
