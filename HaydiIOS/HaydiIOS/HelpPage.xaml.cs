using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();

            HelpButton.Clicked += HelpButton_Clicked;
            LogoutButton.Clicked += LogoutButton_Clicked;
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private async void HelpButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Hakkında", "Arkadaşlarınızı uygulamaya davet ediniz (Store linki gelecek)", "Tamam", "İptal");
        }
    }
}
