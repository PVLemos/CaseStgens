using System;

namespace GoodHamburger.Web.Services;

public class NotificationService
{
    public event Action<string>? OnMessageReceived;

    public void Notify(string message)
    {
        OnMessageReceived?.Invoke(message);
    }
}
