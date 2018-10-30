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
using Android.Graphics.Drawables;
using BloodTrace.Droid;

[assembly: ExportRenderer(typeof(BloodTrace.Controls.RoundButton), typeof(RoundButtonRenderer))]
namespace BloodTrace.Droid
{
   public class RoundButtonRenderer : ButtonRenderer
    {
        public RoundButtonRenderer(Context context): base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(40f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Red);
                gradientDrawable.SetColor(Android.Graphics.Color.Red);
                Control.SetBackground(gradientDrawable);
                //Control.TextColors.WithAlpha(000);
            }
        }
    }
}