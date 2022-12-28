using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AdminUserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        /// <summary>
        /// Registro de un usuario externo es obligatorio el UserName y deberia de ser el numero de usuario del
        /// sistema de seguridad de PECO, el Email es opcional.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost("registraUsuarioExterno")]
        [ProducesResponseType(typeof(UserFullViewModel), 201)]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Parámetros inválidos");
            }
            return StatusCode(201, await _userService.CreateUserAsync(userModel));

        }
            
        /// <summary>
        /// Registro de usurios Interno que hace el administrador de la aplicación
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost("registraUsuarioInterno")]
        [ProducesResponseType(typeof(UserFullViewModel), 201)]
        public async Task<IActionResult> RegistraUsuarioInterno([FromBody] UserRegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Parámetros inválidos");
            }
            return StatusCode(201, await _userService.CreateInternalUserAsync(userModel));

        }

        /// <summary>
        /// Obtiene listado de los usuarios externos
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("getUsuarios")]
        [ProducesResponseType(typeof(IEnumerable<UserFullViewModel>), 200)]
        public async Task<IActionResult> GetUsuarios()
        {
            return StatusCode(200, await _userService.GetUsersAsync());
        }

        /// <summary>
        /// Hacer reset a una contraseña de un usuario externo regresando a la contraseña por default.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("resetPasswordUsuarioExterno/{idUsuario}")]
        [ProducesResponseType(typeof(UserFullViewModel), 200)]
        public async Task<IActionResult> ResetPasswordUsuarioExterno(string idUsuario)
        {
            return StatusCode(200, await _userService.ResetUserPasswordDefaultAsync(idUsuario));
        }

        /// <summary>
        /// Activa o Deactiva un usuario externo Falso es inactivo, True es activo
        /// </summary>
        /// <param name="activeModel"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("ActivarUsuarioExterno/{idUsuario}")]
        [ProducesResponseType(typeof(UserFullViewModel), 200)]
        public async Task<IActionResult> ActivarUsuarioExterno([FromBody] UserActiveModel activeModel, string idUsuario)
        {
            return StatusCode(200, await _userService.ActiveUserAsync(idUsuario, activeModel));
        }

        /// <summary>
        /// Obtiene listado de los roles que se pueden asignar a los usuarios de la plataforma
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("GetRoles")]
        [ProducesResponseType(typeof(IEnumerable<RoleViewModel>), 200)]
        public async Task<IActionResult> GetRoles()
        {
            return StatusCode(200, await _roleService.GetRolesAsync());
        }

        /// <summary>
        /// Edita usuario email y roles
        /// </summary>
        /// <param name="activeModel"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("EditUser/{idUsuario}")]
        [ProducesResponseType(typeof(UserFullViewModel), 200)]
        public async Task<IActionResult> EditUser([FromBody] UserEditModel activeModel, string idUsuario)
        {
            return StatusCode(200, await _userService.EditUserAsync(idUsuario, activeModel));
        }

    }
}
