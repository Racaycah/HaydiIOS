using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HaydiIOS
{
    public partial class PeoplePage : ContentPage
    {
        public ObservableCollection<CustomContact> chosenPeopleList = new ObservableCollection<CustomContact>();
        private ObservableCollection<CustomContact> obsList = new ObservableCollection<CustomContact>();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(chosenPeopleList.Any())
            {
                foreach(CustomContact cc in chosenPeopleList)
                {
                    obsList.Add(cc);
                }
            }
            chosenPeopleList.Clear();
        }

        public PeoplePage(ObservableCollection<CustomContact> people)
        {
            InitializeComponent();

            obsList = people;
            listView.ItemsSource = obsList;
            listView2.ItemsSource = chosenPeopleList;

        }

        public PeoplePage()
        {
            InitializeComponent();

            LoadContacts();

            listView.ItemsSource = obsList;
            listView2.ItemsSource = chosenPeopleList;
        }

        public async void AddPeopleClicked(object sender, EventArgs e)
        {
            if (chosenPeopleList.Count > 0)
            {
                var tmpList = chosenPeopleList.Distinct().ToList();

                foreach(CustomContact cc in tmpList)
                {
                    obsList.Add(cc);
                    chosenPeopleList.Remove(cc);
                }
                await Navigation.PushAsync(new CreateGroupPage(tmpList));
            }
            else
            {
                await DisplayAlert("Hata!", "Lütfen en az 1 kişi seçiniz.", "Tamam");
            }
        }

        public async void LoadContacts()
        {
            if (await CrossContacts.Current.RequestPermission())
            {
                if (CrossContacts.Current.Contacts == null) return;

                var abook = CrossContacts.Current.Contacts.ToList();
                var book = abook.Distinct().ToList();

                foreach (Contact c in book)
                {
                    var tmpCC = new CustomContact();
                    tmpCC.Name = c.DisplayName;
                    if (Data.PeopleList.Where(item => item.Name == tmpCC.Name).Count() == 0 )
                    {
                        if (c.Phones.Exists(m => m.Type == PhoneType.Mobile))
                        {
                            var sad = c.Phones.Find(n => n.Type == PhoneType.Mobile);
                            var numb = sad.Number.Replace(" ", "");
                            tmpCC.Number = numb;
                        }
                        obsList.Add(tmpCC);
                        Data.PeopleList.Add(tmpCC);
                        Data.OtherPeople.Add(tmpCC);
                    }
                }
            }
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as CustomContact;
            chosenPeopleList.Add(selectedItem);
            obsList.Remove(selectedItem);
        }

        public void OnItemSelected2(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as CustomContact;
            obsList.Add(selectedItem);
            chosenPeopleList.Remove(selectedItem);
        }

        public void onSearch(object sender, TextChangedEventArgs e)
        {
            listView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listView.ItemsSource = obsList;
            else
                listView.ItemsSource = obsList.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));

            listView.EndRefresh();

        }
    }
}
