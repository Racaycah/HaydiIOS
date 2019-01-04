using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using HaydiIOS;
using Xamarin.Forms;
using HaydiIOS.iOS;
using Xamarin.Forms.Platform.iOS;

namespace HaydiIOS.iOS
{
    class iOSEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }
    }
}