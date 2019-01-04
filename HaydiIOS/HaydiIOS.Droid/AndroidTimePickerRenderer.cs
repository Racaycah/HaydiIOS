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
using HaydiIOS.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HaydiIOS;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(AndroidTimePickerRenderer))]
namespace HaydiIOS.Droid
{
    class AndroidTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            Control.Gravity = GravityFlags.Center;
        }
    }
}