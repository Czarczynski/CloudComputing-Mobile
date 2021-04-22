using System.ComponentModel.DataAnnotations;

namespace Lab5_app.WebApplication.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage= "Username is required")]
        public string Username { get; set; }
    }
}