using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using HaydiIOS.iOS;
using HaydiIOS;
using Xamarin.Forms.Platform.iOS;

namespace HaydiIOS.iOS
{
    class iOSTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }
    }
}