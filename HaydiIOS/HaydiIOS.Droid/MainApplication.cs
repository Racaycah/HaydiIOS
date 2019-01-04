using System;
using Plugin.Permissions;
using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace HaydiIOS.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            //A great place to initialize Xamarin.Insights and Dependency Services!
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Android.App.Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Android.App.Activity activity)
        {
        }

        public void OnActivityPaused(Android.App.Activity activity)
        {
        }

        public void OnActivityResumed(Android.App.Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Android.App.Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Android.App.Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Android.App.Activity activity)
        {
        }

        //public void OnActivityStopped(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityCreated(Android.App.Activity activity, Bundle savedInstanceState)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityDestroyed(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityPaused(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityResumed(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivitySaveInstanceState(Android.App.Activity activity, Bundle outState)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityStarted(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActivityStopped(Android.App.Activity activity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}