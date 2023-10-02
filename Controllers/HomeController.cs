using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza nuovaPizza)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", nuovaPizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizze.Add(nuovaPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}