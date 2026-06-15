using ControlHabitos.Data;
using ControlHabitos.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace  ControlHabitos.Api.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService (AppDbContext dbContext, IConfiguration configuration)
        {
            this._context = dbContext;
            this._configuration = configuration;
        }

        public async Task<Usuario?> RegistrarUsuario(Usuario usuario, string password)
        {
            var emailExistente = await this._context.Usuarios.AnyAsync(x => x.Email == usuario.Email);

            if(emailExistente)
                return null;

            var hash = CrearPasswordHash(password);

            usuario.PasswordHash = hash;

            await this._context.Usuarios.AddAsync(usuario);
            await this._context.SaveChangesAsync();

            return usuario;
        }

        public async Task<string?> LoginUsuario(string email, string password)
        {
            var usuarioExistente = await this._context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);

            if(usuarioExistente is null)
                return null;

            var hash = CrearPasswordHash(password);

            if(usuarioExistente.PasswordHash != hash)
                return null;

            var token = this.CrearToken(usuarioExistente);

            return token;
        }

        private string  CrearPasswordHash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] hashBytes = SHA256.HashData(passwordBytes);

            return Convert.ToHexString(hashBytes);
        }

        private string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()) 
            };
            
            var expirationMinutes = int.Parse(this._configuration["Jwt:ExpirationMinutes"]);

            //Clave criptográfica que usará JWT
            //convierte texto a byte
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:SecretKey"]));

            //firma con esta clave y algoritmo
            var credentials = new SigningCredentials(
                                                        key,
                                                        SecurityAlgorithms.HmacSha256
                                                );


            var token = new JwtSecurityToken(
                                                claims: claims,
                                                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                                                signingCredentials: credentials
                                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}