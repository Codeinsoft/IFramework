using System.Collections.Generic;

namespace IFramework.Infrastructure.Utility.Notification
{
    /// <summary>
    /// Bildirim gönderme işlemi yapan sınıflar INotification interface'inden türetilmelidir.
    /// </summary>
    public interface INotification
    {
        void Send(string toUser, string content);
        void Send(List<string> toUsers, string content);
    }
}
