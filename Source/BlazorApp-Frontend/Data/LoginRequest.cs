using System.ComponentModel.DataAnnotations;

namespace BlazorApp_Frontend.Data
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Du måste ange en e-post")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Ange en giltig e-post")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
