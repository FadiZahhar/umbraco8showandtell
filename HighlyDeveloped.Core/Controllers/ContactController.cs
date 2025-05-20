using HighlyDeveloped.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
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
            //create a new contact form in umbraco

            //get a handle to "contact forms"
            var contactForms = Umbraco.ContentAtRoot().DescendantsOrSelfOfType("contactForms").FirstOrDefault();

            if(contactForms!=null)
            {
        
                var newContact = Services.ContentService.Create("Contact", contactForms.Id, "contactForm");
                newContact.SetValue("contactName", vm.Name);
                newContact.SetValue("contactEmail", vm.EmailAddress);
                newContact.SetValue("contactSubject", vm.Subject);
                newContact.SetValue("contactComments", vm.Comment);
                Services.ContentService.SaveAndPublish(newContact);

            }

            //send out email to site admin
            TempData["status"] = "OK";
            //return confirmation message to user

            return RedirectToCurrentUmbracoPage();
            //return null;
        }
    }
}
