using Microsoft.AspNetCore.Mvc;
using Projeto_Livraria.Models;
using System.Diagnostics;

namespace Projeto_Livraria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") is null)
            {
                return RedirectToAction("Login", "Conta");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}