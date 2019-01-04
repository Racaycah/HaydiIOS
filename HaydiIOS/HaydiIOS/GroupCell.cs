using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HaydiIOS
{
    public class GroupCell : ViewCell
    {
        public static BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(CustomGroup), "Name");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
    }
}
