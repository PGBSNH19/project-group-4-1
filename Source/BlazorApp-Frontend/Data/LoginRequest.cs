using System.ComponentModel.DataAnnotations;

namespace BlazorApp_Frontend.Data
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Du måste ange en E-post")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Ange en giltig E-post")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Du måste ange ett Lösenord")]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
