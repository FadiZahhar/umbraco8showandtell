using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlyDeveloped.Core.ViewModel
{
    public class NewsletterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

}
