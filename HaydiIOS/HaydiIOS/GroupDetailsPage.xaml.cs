using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class GroupDetailsPage : ContentPage
    {
        public CustomGroup _Group = new CustomGroup();

        public GroupDetailsPage(CustomGroup Group)
        {
            InitializeComponent();

            //if (Data.GroupEdited)
            //{
            //    Group = Data.db.GetGroup(Group.ID);
            //    Data.GroupEdited = false;
            //}

            _Group = Group;
            GroupNameLabel.Text = Group.Name;
            listView.ItemsSource = Group.People;
            FavoriteSwitch.IsToggled = Group.IsFavourite;
            FavoriteSwitch.Toggled += FavoriteSwitch_Toggled;
        }

        public async void DeleteGroup(object sender, EventArgs e)
        {
            var selection = await DisplayAlert("Grubu Sil", "Grubu silmek istediğinize emin misiniz ?", "Evet", "Hayır");

            if (selection)
            {
                Data.db.DeleteGroup(_Group.ID);
                //Data.GroupsList.Remove(Data.FindGroup(_Group.Name));
                await DisplayAlert("Tamam", "Grup başarıyla silindi.", "Tamam");
                await Navigation.PopAsync();
            }
        }

        public async void EditGroup(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditGroupPage(_Group));
        }

        private void FavoriteSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            var data = Data.FindGroup(_Group.Name); //Where(c => c.Name == _Group.Name);
            bool toggled = data.IsFavourite;
            data.IsFavourite = !toggled;
            Data.db.UpdateGroup(data);

            int index = Data.GroupsList.IndexOf(_Group);
            if(index != -1)
            {
                Data.GroupsList[index].IsFavourite = !toggled;
            }
        }
    }
}
