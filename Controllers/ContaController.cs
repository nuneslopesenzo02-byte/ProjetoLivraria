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

            var usuario = await db.Login.FirstOrDefaultAsync(u => u.Email == model.Email.Trim());

            if (usuario is null ||
                hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, model.Senha) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View(model);
            }

            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View(new CadastroViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(CadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var emailNormalizado = model.Email.Trim();

            var emailJaExiste = await db.Login.AnyAsync(u => u.Email == emailNormalizado);
            if (emailJaExiste)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                return View(model);
            }

            var novoUsuario = new Login
            {
                Nome = model.Nome.Trim(),
                Email = emailNormalizado
            };

            novoUsuario.SenhaHash = hasher.HashPassword(novoUsuario, model.Senha);

            db.Login.Add(novoUsuario);
            await db.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Conta criada com sucesso! Faça seu login.";
            return RedirectToAction("Login");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}