using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ViewModel representing the data required for a contact form submission.
/// </summary>
public class ContactFormViewModel
{
    /// <summary>
    /// Gets or sets the name of the person submitting the contact form.
    /// </summary>
    /// <remarks>Maximum length is 80 characters.</remarks>
    [Required]
    [MaxLength(80, ErrorMessage = "Please try and limit to 80 characters")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the person submitting the contact form.
    /// </summary>
    /// <remarks>Must be a valid email address.</remarks>
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the comment or message provided by the user.
    /// </summary>
    /// <remarks>Maximum length is 500 characters.</remarks>
    [Required]
    [MaxLength(500, ErrorMessage ="Please try and limit your comments to 500 characters")]
    public string Comment { get; set; }

    /// <summary>
    /// Gets or sets the subject of the contact form submission.
    /// </summary>
    /// <remarks>Maximum length is 255 characters.</remarks>
    [MaxLength(255, ErrorMessage = "Please try and limit to 255 characters")]
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the reCAPTCHA site key for validating the form submission.
    /// </summary>
    public string RecaptchaSiteKey { get; set; }
}
namespace HighlyDeveloped.Core.ViewModel
{
    public class ContactFormViewModel
    {
        [Required]
        [MaxLength(80, ErrorMessage = "Please try and limit to 80 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage ="Please try and limit your comments to 500 characters")]
        public string Comment { get; set; }
        [MaxLength(255, ErrorMessage = "Please try and limit to 255 characters")]
        public string Subject { get; set; }
        public string RecaptchaSiteKey { get; set; }
    }
}
