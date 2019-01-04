using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class EditGroupPage : ContentPage
    {
        public CustomGroup _Group = new CustomGroup();
        public ObservableCollection<CustomContact> People = new ObservableCollection<CustomContact>();
        public ObservableCollection<CustomContact> Others = new ObservableCollection<CustomContact>();

        public EditGroupPage(CustomGroup Group)
        {
            InitializeComponent();

            _Group = Group;

            HeaderLabel.Text = Group.Name;

            foreach (CustomContact cc in Group.People)
            {
                People.Add(cc);
                Data.OtherPeople.Remove(Data.OtherPeople.Where(i => i.Name == cc.Name).Single());
            }

            GroupPeople.ItemsSource = People;
            OtherPeople.ItemsSource = Data.OtherPeople;

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => LabelClicked();
            HeaderLabel.GestureRecognizers.Add(tgr);

            ChangeName.Placeholder = HeaderLabel.Text;
        }

        protected override void OnDisappearing()
        {
            Data.OtherPeople = Data.PeopleList;

            if (Data.GroupEdited)
            {
                Data.GroupEdited = false;
                Navigation.PopAsync();
            }
        }

        private void LabelClicked()
        {
            ChangeName.IsVisible = true;
            ChangeName.Focus();
        }

        public void Add(object sender, SelectedItemChangedEventArgs e)
        {
            if (OtherPeople.SelectedItem == null)
                return;

            var item = e.SelectedItem as CustomContact;
            Data.OtherPeople.Remove(Data.OtherPeople.Where(i => i.Name == item.Name).Single());
            People.Add(item);

            OtherPeople.SelectedItem = null;
        }

        public void Remove(object sender, SelectedItemChangedEventArgs e)
        {
            if (GroupPeople.SelectedItem == null)
                return;

            var item = e.SelectedItem as CustomContact;
            People.Remove(item);
            Data.OtherPeople.Add(item);

            GroupPeople.SelectedItem = null;
        }

        public async void Done(object sender, EventArgs e)
        {
            if (People.Count == 0)
            {
                await DisplayAlert("Hata!", "Grup boş olamaz.", "Tamam");
            }
            else
            {
                var data = Data.FindGroup(_Group.Name);
                int i = Data.GroupsList.IndexOf(_Group);

                if (ChangeName.Text != null)
                {
                    data.Name = ChangeName.Text;
                }
                data.People = People.ToList();
                Data.GroupsList[i] = data;
                Data.db.UpdateGroup(data);
                Data.GroupEdited = true;
                //var test = Data.db.GetGroup(data.ID);
                await DisplayAlert("Tamam", "Grup başarıyla güncellendi.", "Tamam");
                await Navigation.PopAsync();
            }
        }

        public void onSearch(object sender, TextChangedEventArgs e)
        {
            OtherPeople.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                OtherPeople.ItemsSource = Data.OtherPeople;
            else
                OtherPeople.ItemsSource = Data.OtherPeople.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));

            OtherPeople.EndRefresh();
        }
    }
}
