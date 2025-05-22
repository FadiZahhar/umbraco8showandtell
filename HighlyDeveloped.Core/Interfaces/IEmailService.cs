using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighlyDeveloped.Core.ViewModel;

namespace HighlyDeveloped.Core.Interfaces
{
    /// <summary>
    /// Handles all emaill operations
    /// </summary>
    public interface IEmailService
    {
        void SendContactNotificationToAdmin(ContactFormViewModel vm);
        void SendVerifyEmailAddressNotification(string membersEmail, string verificationToken);

        void SendResetPasswordNotification(string membersEmail, string resetToken);
    }
}
