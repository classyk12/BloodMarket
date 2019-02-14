﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace BloodTrace.Droid
{
    [Activity(Label ="BloodTrace",Icon ="@drawable/icon",Theme ="@style/logo",MainLauncher =true,NoHistory =true)]
   public class SplashActivity : AppCompatActivity
    {
      protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}