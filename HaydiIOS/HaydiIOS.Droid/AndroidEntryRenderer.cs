using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HaydiIOS;
using HaydiIOS.Droid;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(AndroidEntryRenderer))]
namespace HaydiIOS.Droid
{
    class AndroidEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.Gravity = GravityFlags.Center;
            Control.SetSingleLine();
        }
    }
}