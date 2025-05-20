using HighlyDeveloped.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace HighlyDeveloped.Core.Controllers
{
    //this operations for cntact form
    public class ContactController : SurfaceController
    {
        //render contact form
        public ActionResult RenderContactForm()
        {
            var vm =  new ContactFormViewModel();
            return PartialView("~/Views/Partials/Contact Form.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //when submitting it is handled here
        public ActionResult HandleContactForm(ContactFormViewModel vm)
        {
            //VALIDATION IS FROM MODEL VALIDATION
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Please check the form");
                return CurrentUmbracoPage();


            }
            return null;
        }
    }
}
