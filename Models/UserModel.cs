using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class UserRegisterModel
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; }

        [Required]
        public IEnumerable<RoleSelectedModel> Roles { get; set; }
    }

    public class UserEditModel
    {
        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; }
        [Required]
        public IEnumerable<RoleSelectedModel> Roles { get; set; }
    }

    public class UserFullViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public bool Active { get; set; }
        public IEnumerable<RoleUserViewModel> Roles { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class UserChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class UserActiveModel
    {
        public bool Active { get; set; } = true;
    }
    public class InternalUserRegisterModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserInternalModel
    {
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }

    public class UserPecoLoginResponseModel
    {
        public string USUARIOID { get; set; }
        public string NOMBRE { get; set; }
        public string NOEMPLEADO { get; set; }
        public string NIVELFIRMA { get; set; }
        public string PUESTO { get; set; }
    }
}
