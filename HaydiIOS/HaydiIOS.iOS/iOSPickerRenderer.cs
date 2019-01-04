using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using HaydiIOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HaydiIOS.iOS;

namespace HaydiIOS.iOS
{
    class iOSPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }
    }
}