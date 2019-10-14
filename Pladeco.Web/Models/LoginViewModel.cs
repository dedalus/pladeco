namespace Pladeco.Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Recordarme")]
        public bool RememberMe { get; set; }
    }
}
