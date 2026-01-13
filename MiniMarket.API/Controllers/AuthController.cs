using Microsoft.AspNetCore.Mvc;
using MiniMarket.API.DTOs;
using MiniMarket.API.Mappers;
using MiniMarket.API.Services;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly AuthService _authService;

        public AuthController(IUtilisateurService utilisateurService, AuthService authService)
        {
            _utilisateurService = utilisateurService;
            _authService = authService;
        }

        [HttpPost("register", Name = "Register")]
        public ActionResult Register([FromBody] AuthRegisterForm registerForm)
        {
            if (registerForm is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Utilisateur newUser = registerForm.ToUtilisateur();
            _utilisateurService.Create(newUser);

            return Created();
        }

        [HttpPost("login", Name = "Login")]
        public ActionResult Login([FromBody] AuthLoginForm loginForm)
        {
            if (loginForm is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Utilisateur utilisateur = _utilisateurService.Login(loginForm.Username, loginForm.Password);

            string token = _authService.GenerateToken(utilisateur);

            var cookieOptions = new CookieOptions
            {
                // CRUCIAL : Empêche le JavaScript client d'accéder au cookie
                HttpOnly = true,

                // Recommandé : Le cookie n'est envoyé que sur HTTPS (à mettre à false pour le dév localhost sans https)
                Secure = true,

                // Recommandé : Protège contre les attaques CSRF
                SameSite = SameSiteMode.Strict,

                // Durée de vie du cookie
                Expires = DateTime.UtcNow.AddHours(1)
            };

            // Ajout du token dans un cookie
            Response.Cookies.Append("accessToken", token, cookieOptions);

            return Ok();
        }

        [HttpPost("logout", Name = "Logout")]
        public ActionResult Logout()
        {
            // Supprime le cookie "accessToken"
            Response.Cookies.Delete("accessToken");

            return Ok();
        }
    }
}
