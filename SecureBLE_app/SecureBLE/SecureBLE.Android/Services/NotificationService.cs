using System;
using Android.App;
using SecureBLE.Droid.Services;
using SecureBLE.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
namespace SecureBLE.Droid.Services
{
    public class NotificationService : INotificationService
    {
        public void Snackbar(string message, int duration, string actionText = null, Action<object> action = null)
        {
            //TODO: replace Context with local context
            var contentView = (Forms.Context as Activity)?.FindViewById(Android.Resource.Id.Content);
            var snackbar = Android.Support.Design.Widget.Snackbar.Make(contentView, message, duration);

            if (actionText != null)
                snackbar.SetAction(actionText, action);

            snackbar.Show();
        }
    }
}