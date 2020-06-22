using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Aircharts.Views;
using Xamarin.Forms;
using Lottie.Forms.Droid;
using Plugin.Screenshot;

namespace Aircharts.Droid
{
    [Activity(Label = "Aircharts", Icon = "@drawable/LogoAircharts", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            Window.SetBackgroundDrawableResource(Resource.Drawable.BlurBackground);
            SetStatusBarColor(Android.Graphics.Color.Transparent);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
        }
        protected override void OnPostResume()
        {
            base.OnPostResume();
            if (Intent.Extras != null)
            {
                string uri = Intent.Extras.GetString("redirectUri");
                if (!string.IsNullOrEmpty(uri))
                {
                    App.Current.MainPage.Navigation.PushAsync(new RedirectPage(uri));
                }
                Intent.RemoveExtra("redirectUri");
            }


        }
    }
}