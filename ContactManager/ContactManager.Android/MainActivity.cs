using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;

namespace ContactManager.Droid
{
    [Activity(Label = "ContactManager", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static string ContactNumberMainActivity;
        protected override void OnCreate(Bundle bundle)
        {
            CrossCurrentActivity.Current.Init(this, bundle);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case 88:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {   
                            PhoneServiceDroid.CallContactAfterPermissionOk(ContactNumberMainActivity);
                        }
                        else
                        {
                            Toast.MakeText(CrossCurrentActivity.Current.AppContext, "Permission to call is currently disabled", ToastLength.Short).Show();
                        }
                        break;
                    }
                default:
                    break;
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

