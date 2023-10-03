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
        private CustomLogger _myLogger;

        public PizzaController()
        {
            _myLogger = new CustomLogger();
        }

        public IActionResult Index()
        {
            _myLogger.WriteLog("L'utente è sulla pagian index");
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


        [HttpGet] 
        public IActionResult Update(int id)
        {

            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaModificare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaModificare == null)
                {
                    return NotFound("La pizza che vuoi modificare non è stata trovata");
                }
                else
                {
                    return View("Update", pizzaDaModificare);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza modificaPizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaModificare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzaDaModificare != null)
                {
                    pizzaDaModificare.Nome = modificaPizza.Nome;
                    pizzaDaModificare.Descrizione = modificaPizza.Descrizione;
                    pizzaDaModificare.Image = modificaPizza.Image;
                    pizzaDaModificare.Prezzo = modificaPizza.Prezzo;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }else
                {
                    return NotFound("Non è stata trovata la pizza da modificare");
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaCancellare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaCancellare != null)
                {
                    db.Pizze.Remove(pizzaDaCancellare);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza da eliminare non è stata trovata!");
                }
            }


        }


    }




}
