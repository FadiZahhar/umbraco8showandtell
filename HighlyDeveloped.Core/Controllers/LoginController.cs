using HighlyDeveloped.Core.Interfaces;
using HighlyDeveloped.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Media.EmbedProviders;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Core.Logging;

namespace HighlyDeveloped.Core.Controllers
{
    /// <summary>
    /// Bespoke login process
    /// </summary>
    public class LoginController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Login/";
        private IEmailService _emailService;
        public LoginController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        #region Login
        public ActionResult RenderLogin()
        {
            var vm = new LoginViewModel();
            vm.RedirectUrl = HttpContext.Request.Url.AbsolutePath;
            return PartialView(PARTIAL_VIEW_FOLDER + "Login.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLogin(LoginViewModel vm)
        {
            //Check if model is ok
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //Check if the member exists with that username
            var member = Services.MemberService.GetByUsername(vm.Username);
            if (member == null)
            {
                ModelState.AddModelError("Login", "Cannot find that username in the system");
                return CurrentUmbracoPage();
            }

            //Check if the member is locked out
            if (member.IsLockedOut)
            {
                ModelState.AddModelError("Login", "The account is locked, please use forgotten password to reset");
                return CurrentUmbracoPage();
            }

            //Check if they have validated their email address
            var emailVerified = member.GetValue<bool>("emailVerified");
            if (!emailVerified)
            {
                ModelState.AddModelError("Login", "Please verify your email before logging in.");
                return CurrentUmbracoPage();
            }

            //Check if credentials are ok
            //Log them in
            if (!Members.Login(vm.Username, vm.Password))
            {
                ModelState.AddModelError("Login", "The username/password your provided is not correct.");
                return CurrentUmbracoPage();
            }

            if (!string.IsNullOrEmpty(vm.RedirectUrl))
            {
                return Redirect(vm.RedirectUrl);
            }
            return RedirectToCurrentUmbracoPage();
        }
        #endregion

        

        
    }
}