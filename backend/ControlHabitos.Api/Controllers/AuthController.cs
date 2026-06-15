using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Api.Services;
using ControlHabitos.Api.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace ControlHabitos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(AuthService authService, IConfiguration configuration)
        {
            this._authService = authService;
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioResponseDto>> NuevoUsuario([FromBody] RegisterUsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Email = usuarioDto.Email
                
            };

            var usuarioRegistrado = await this._authService.RegistrarUsuario(usuario, usuarioDto.Password);

            if(usuarioRegistrado is null)
                return Conflict("El usuario que intenta cargar ya existe.");

            var usuarioResponse = MapearUsuarioResponse(usuarioRegistrado);

            return Ok(usuarioResponse);
        }

        private UsuarioResponseDto MapearUsuarioResponse(Usuario usuario)
        {
                return new UsuarioResponseDto
                            {
                                Id = usuario.Id,
                                Nombre = usuario.Nombre,
                                Email = usuario.Email
                            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioResponseDto>> Login([FromBody] LoginUsuarioDto loginDto)
        {
            var token = await this._authService.LoginUsuario(loginDto.Email, loginDto.Password);

            if(token is null)
                return Unauthorized("Email o contraseña incorrectos.");

            var expirationMinutes = int.Parse(this._configuration["Jwt:ExpirationMinutes"]);

            Response.Cookies.Append(
                                        "auth",
                                        token,
                                        new CookieOptions()
                                        {
                                            HttpOnly = true,
                                            Secure = true,
                                            SameSite = SameSiteMode.None,
                                            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes)
                                        }
                                    );

            return Ok();
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Autenticado");
        }
    }
}