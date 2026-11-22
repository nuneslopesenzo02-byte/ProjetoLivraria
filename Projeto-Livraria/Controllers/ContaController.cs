using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Livraria.Data;
using Projeto_Livraria.Models;

namespace Projeto_Livraria.Controllers
{
    public class ContaController(LivrariaContext db, IPasswordHasher<Login> hasher) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await db.Logins.FirstOrDefaultAsync(u => u.Email == model.Email.Trim());

            if (usuario is null ||
                hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, model.Senha) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View(model);
            }

            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}