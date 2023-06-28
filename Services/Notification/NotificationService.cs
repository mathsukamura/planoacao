using apiplanoacao.Services.PlanoDeAcao;
using System.Collections.Generic;
using System.Linq;

namespace apiplanoacao.Services.Notification
{
    public class Notification
    {
        public string Property { get; set; }
        public string Message { get; set; }
    }

    public interface INotificationService
    {
        bool IsValid { get; }
        bool IsNotValid { get; }
        void AddNotification(Notification notification);
        void AddNotification(string propery, string message);
        IReadOnlyCollection<Notification> GetAllNotifications();
    }

    public class NotificationService : INotificationService
    {
        private readonly List<Notification> notifications = new List<Notification>();

        public bool IsValid => !notifications.Any();

        public bool IsNotValid => !IsValid;

        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public void AddNotification(string propery, string message) 
        {
            var model = new Notification()
            {
                Property = propery,
                Message = message
            };

            AddNotification(model);
        }

        public IReadOnlyCollection<Notification> GetAllNotifications()
        {
            return notifications.AsReadOnly();
        }
    }
}
