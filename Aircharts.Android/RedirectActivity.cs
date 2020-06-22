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

namespace Aircharts.Droid
{
    [Activity(Label = "RedirectActivity", Theme = "@style/SplashTheme")]
    [IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "xamarinapiapp",
    DataHost = "callback")]
    public class RedirectActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.Data != null)
            {
                var uri = Intent.Data;
                if (uri != null)
                {
                    Intent i = new Intent(this, typeof(MainActivity));
                    i.AddFlags(ActivityFlags.ReorderToFront);
                    i.PutExtra("redirectUri", uri.GetQueryParameter("code"));
                    this.StartActivity(i);
                }
            }
            this.FinishActivity(0);
        }
    }
}