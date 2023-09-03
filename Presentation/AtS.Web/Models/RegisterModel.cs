using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TWYK.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [AllowHtml]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public bool Agree { get; set; }
    }
}