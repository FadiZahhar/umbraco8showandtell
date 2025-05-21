using HighlyDeveloped.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Core.Logging;
using System.Net.Mail;
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

            try
            {
                //create a new contact form in umbraco

                //get a handle to "contact forms"
                var contactForms = Umbraco.ContentAtRoot().DescendantsOrSelfOfType("contactForms").FirstOrDefault();

                if (contactForms != null)
                {

                    var newContact = Services.ContentService.Create("Contact", contactForms.Id, "contactForm");
                    newContact.SetValue("contactName", vm.Name);
                    newContact.SetValue("contactEmail", vm.EmailAddress);
                    newContact.SetValue("contactSubject", vm.Subject);
                    newContact.SetValue("contactComments", vm.Comment);
                    Services.ContentService.SaveAndPublish(newContact);

                }

                //send out email to site admin


                SendContactFormReceivedEmail(vm);



                //return confirmation message to user
                TempData["status"] = "OK";
                return RedirectToCurrentUmbracoPage();
                //return null;
            }

            catch (Exception exc){
                Logger.Error<ContactController>("There was an error in contact form", exc.Message);
                ModelState.AddModelError("Error", "Sorry, there was a problem. Would you please try again later");
            }

            return CurrentUmbracoPage();
        }

        private void SendContactFormReceivedEmail(ContactFormViewModel vm)
        {
            //Get site settings
            var siteSettings = Umbraco.ContentAtRoot().DescendantsOrSelfOfType("siteSettings").FirstOrDefault();
            if(siteSettings == null)
            {
                throw new Exception("There are no site settings");
            }

            var fromAddress = siteSettings.Value<string>("emailSettingsFromAddress");
            var toAddresses = siteSettings.Value<string>("emailSettingsAdminAccount");

            if (string.IsNullOrEmpty(fromAddress))
            {
                throw new Exception("There needs to be a from address in site settings");

            }

            if (string.IsNullOrEmpty(toAddresses))
            {
                throw new Exception("There needs to be a to address in site settings");

            }

            //read email from and to addressess
            //Construct the actual email
            var emailSubject = "there has been a contact form submitted";
            var emailBody = $"a new contact has been received from {vm.Name}. Their comments were {vm.Comment}";
            var smtpMessage = new MailMessage();
            smtpMessage.Subject = emailSubject;
            smtpMessage.Body = emailBody;
            smtpMessage.From = new MailAddress (fromAddress);
            
            var toList = toAddresses.Split(',');
            foreach (var item in toList)
            {
                if(!string.IsNullOrEmpty(item))
                    smtpMessage.To.Add(item);
            }



            //Send via whatever email service
            using (var smtp = new SmtpClient()) { 
            smtp.Send(smtpMessage);
            }
        }
    }
}
