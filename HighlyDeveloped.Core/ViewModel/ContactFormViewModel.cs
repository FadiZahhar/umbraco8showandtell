using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlyDeveloped.Core.ViewModel
{
    public class ContactFormViewModel
    {
        //capture the following data
        [Required]
        [MaxLength(80 ,  ErrorMessage = "Please limit char to 80")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Please limit comments to 500 char")]
        public string Comment { get; set; }

        [MaxLength(255, ErrorMessage = "Please limit subject to 255 char")]
        public string Subject { get; set; }


        //par for recaptcha
        public string RecaptchaSiteKey { get; set; }

    }
}
