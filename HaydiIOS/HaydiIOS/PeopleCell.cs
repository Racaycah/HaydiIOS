using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HaydiIOS
{
    public class PeopleCell : ViewCell
    {
        public static BindableProperty NameProperty = BindableProperty.Create("DisplayName", typeof(string), typeof(PeopleCell), "");

        public string DisplayName
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty ImageFileNameProperty = BindableProperty.Create("ImageFileName", typeof(string), typeof(PeopleCell), "");

        public string ImageFileName
        {
            get { return (string)GetValue(ImageFileNameProperty); }
            set { SetValue(ImageFileNameProperty, value); }
        }
    }
}
