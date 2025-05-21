using HighlyDeveloped.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlyDeveloped.Core.Interfaces
{
 
    public interface IEmailService
    {
        void SendContactNotificationToAdmin(ContactFormViewModel vm);
    }
}