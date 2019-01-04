using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class GroupsPage : ContentPage
    {
        public GroupsPage()
        {
            InitializeComponent();

            LoadGroups();

            GroupsListView.ItemsSource = Data.GroupsList;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Navigation.PopToRootAsync();

           // if (Data.GroupEdited)
           // {
                Data.GroupsList.Clear();
                LoadGroups();
           // }
        }

        public void LoadGroups()
        {
            var groups = Data.db.GetGroups();

            if (groups.Any())
            {
                foreach (CustomGroup group in groups)
                {
                    Data.GroupsList.Add(group);
                }
            }
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var group = e.SelectedItem as CustomGroup;
            GroupsListView.SelectedItem = null;
            await Navigation.PushAsync(new GroupDetailsPage(group), true);
        }

        public async void AddGroupClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PeoplePage(Data.PeopleList));
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var selection = await DisplayAlert("Grubu Sil", "Bu grubu silmek istediğinize emin misiniz?\n", "Evet", "Hayır");

            if (selection)
            {
                var mi = (MenuItem)sender;
                var group = (CustomGroup)mi.CommandParameter;
                Data.db.DeleteGroup(group.ID);
                //Data.GroupsList.Remove(Data.FindGroup(group.Name));
            }
        }
    }
}
