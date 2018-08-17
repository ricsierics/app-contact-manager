using Android;
using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Widget;
using ContactManager.Droid;
using ContactManager.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneServiceDroid))]
namespace ContactManager.Droid
{
    public class PhoneServiceDroid : ICallService
    {
        public void CallContact(string contactNumber)
        {   
            var context = CrossCurrentActivity.Current.AppContext;
            var activity = CrossCurrentActivity.Current.Activity;

            if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.CallPhone) == (int)Android.Content.PM.Permission.Granted)
            {
                //Has permission to call
                Toast.MakeText(CrossCurrentActivity.Current.AppContext, "Permission to call granted", ToastLength.Short).Show();
                CallContactAfterPermissionOk(contactNumber);
            }
            else
            {
                //Has no permission to call. Display rationale
                MainActivity.ContactNumberMainActivity = contactNumber; //Need to pass this param, no cleaner way
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.CallPhone }, 88);
            }
        }

        public static void CallContactAfterPermissionOk(string contactNumber)
        {
            var uri = Android.Net.Uri.Parse($"tel:{contactNumber}");
            var intent = new Intent(Intent.ActionCall, uri);
            //Xamarin.Forms.Forms.Context.StartActivity(intent); //obsolete
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}