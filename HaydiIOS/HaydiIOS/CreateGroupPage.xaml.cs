using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class CreateGroupPage : ContentPage
    {
        public ObservableCollection<CustomContact> obsList = new ObservableCollection<CustomContact>();
        public CustomGroup GroupPeople = new CustomGroup();

        public CreateGroupPage(List<CustomContact> Chosen)
        {
            InitializeComponent();
            GroupPeople.People = Chosen;
            GroupName.Placeholder = "Grup Adı";

            foreach (CustomContact c in Chosen)
            {
                obsList.Add(c);
            }

            SelectedPeople.ItemsSource = obsList;
        }

        public void RemovePerson(object sender, SelectedItemChangedEventArgs e)
        {
            var person = e.SelectedItem as CustomContact;
            GroupPeople.People.Remove(person);
            obsList.Remove(person);
        }

        public async void CreateClicked(object sender, EventArgs e)
        {
            if (GroupName.Text == null)
            {
                await DisplayAlert("Hata!", "Lütfen grup adı giriniz.", "Tamam");
            }
            else if (GroupPeople.People == null)
            {
                await DisplayAlert("Hata!", "Lütfen en az 1 kişi seçiniz.", "Tamam");
            }
            else
            {
                GroupPeople.Name = GroupName.Text;
                Data.GroupsList.Add(GroupPeople);
                Data.db.AddGroup(GroupPeople);
                await DisplayAlert("Tamam", "Grubunuz kaydedildi.", "Tamam");
                await Navigation.PopAsync();
            }
        }
    }
}
