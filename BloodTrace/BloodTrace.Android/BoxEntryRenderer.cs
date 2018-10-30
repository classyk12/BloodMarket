using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BloodTrace.Controls;
using BloodTrace.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BoxEntry), typeof(BoxEntryRenderer))]
namespace BloodTrace.Droid
{
   public class BoxEntryRenderer: EntryRenderer
    {
        public BoxEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(25f);
                gradientDrawable.SetStroke(2, Android.Graphics.Color.Red);
                gradientDrawable.SetColor(Android.Graphics.Color.White);

                Control.SetBackground(gradientDrawable);
            }
            }
    }
}