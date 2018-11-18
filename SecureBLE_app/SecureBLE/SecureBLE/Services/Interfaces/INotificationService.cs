using System;

namespace SecureBLE.Services.Interfaces
{
    public interface INotificationService
    {
        void Snackbar(string message, int duration = 2000, string actionText = null, Action<object> action = null);
    }
}
