using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace SecureBLE.Droid
{
    [Activity(Label = "SecureBLE", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

		protected override void OnStart()
		{
			base.OnStart();
			RequestPermission(Manifest.Permission.AccessFineLocation);
			RequestPermission(Manifest.Permission.AccessCoarseLocation);
		}

		private void RequestPermission(string permission)
		{
			if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
			{
				ActivityCompat.RequestPermissions(this, new string[] { permission }, 0);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine($"{ permission } already granted.");
			}
		}
	}
}