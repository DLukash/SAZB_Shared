using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace SAZB_shared
{
    [Activity(Label = "SAZB_shared", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

