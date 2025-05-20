using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HighlyDeveloped.Core.ViewModel;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace HighlyDeveloped.Core.Controllers
{

    /// <summary>
    /// This is for all operations regarding the contact form
    /// </summary>
    public class ContactController : SurfaceController
    {
        public ActionResult RenderContactForm()
        {
        var vm = new ContactFormViewModel();
        return PartialView("~/Views/Partials/Contact Form.cshtml", vm);
    }
    }
}
