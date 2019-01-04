using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using HaydiIOS;
using HaydiIOS.iOS;
using Xamarin.Forms.Platform.iOS;

namespace HaydiIOS.iOS
{
    class iOSDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }
    }
}