using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Database;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using PizzaContext db = new PizzaContext();

            List<Pizza> pizze = db.Pizze.ToList<Pizza>();


                return View("Index", pizze);
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
