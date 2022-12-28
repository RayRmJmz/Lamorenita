using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime Vigencia { get; set; }
        public string Usuario { get; set; }
        public IList<string> Roles { get; set; }
    }
}
