using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Acr.UserDialogs;

namespace BloodTrace.Droid
{
    [Activity(Label = "BloodTrace", Icon = "@drawable/icon", Theme = "@style/logo", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
    
    }
}

