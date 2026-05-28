namespace FutureStage2026.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
