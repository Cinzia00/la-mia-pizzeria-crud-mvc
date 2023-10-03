using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
     
        private CustomLogger _myLogger;

        public HomeController()
        {
            _myLogger = new CustomLogger();
        }

        public IActionResult Index()
        {
            _myLogger.WriteLog("L?utente è sulla pagina Home");
            using PizzaContext db = new PizzaContext();
            List<Pizza> pizze = db.Pizze.ToList<Pizza>();

            return View("Index", pizze);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserIndex()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DettagliPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? dettaglioPizza = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();

                if (dettaglioPizza == null)
                {
                    return NotFound($"La pizza con id {id} non è stata trovata!");
                }
                else
                {
                    return View("DettagliPizza", dettaglioPizza);
                }
            }
        }

    }
}