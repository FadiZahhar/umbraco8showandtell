using HighlyDeveloped.Core.ViewModel;
using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace HighlyDeveloped.Core.Controllers
{

    public class NewsletterController : SurfaceController
    {
        [HttpPost]
        public ActionResult SubmitNewsletter(string email, string phoneNumber)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["error"] = "Email is required.";
                return RedirectToCurrentUmbracoPage();
            }

            try
            {
                var contentService = Services.ContentService;
                var parentId = 1264; // Replace with your parent node ID

                var entry = contentService.Create(email, parentId, "newsletterSubmission");
                entry.SetValue("email", email);
                entry.SetValue("phoneNumber", phoneNumber);
                contentService.SaveAndPublish(entry);

                TempData["success"] = "Thank you for subscribing!";
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(NewsletterController), ex, "Newsletter submission failed");
                TempData["error"] = "Something went wrong. Please try again.";
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}
