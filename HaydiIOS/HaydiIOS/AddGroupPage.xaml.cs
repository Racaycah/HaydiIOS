using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class AddGroupPage : ContentPage
    {
        public AddGroupPage()
        {
            InitializeComponent();
            GroupNameEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Navigation.PopAsync();
        }

        public void OnItemSelected(object sender, EventArgs e)
        {

        }

        public async void AddGroupClicked(object sender, EventArgs e)
        {
            var groupPage = new GroupsPage();
            groupPage.BindingContext = GroupNameEntry.Text;
            await Navigation.PushAsync(groupPage);
        }
    }
}
