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
using Xamarin.Forms.Platform.Android;
using HaydiIOS.Droid;
using HaydiIOS;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(AndroidPickerRenderer))]
namespace HaydiIOS.Droid
{
    class AndroidPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control.Gravity = GravityFlags.Center;
        }
    }
}