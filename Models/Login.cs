using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is a Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is a Required")]
        public string Password { get; set; }
    }
}
