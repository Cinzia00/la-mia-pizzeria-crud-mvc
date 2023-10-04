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
            using (PizzaContext db = new PizzaContext())
            {
                List<Categoria> categorie = db.Categorie.ToList();

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categorie = categorie;

                return View("Create", model); 
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {

            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categorie = db.Categorie.ToList();
                    data.Categorie = categorie;

                    return View("Create", data);
                }

            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizze.Add(data.Pizza);
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
                    List<Categoria> categorie = db.Categorie.ToList();

                    PizzaFormModel model = new PizzaFormModel { Pizza =  pizzaDaModificare, Categorie = categorie};

                    return View("Update", model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {

            if(!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categorie = db.Categorie.ToList();
                    data.Categorie = categorie;
                    return View("Update", data);
                }
            }
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaModificare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzaDaModificare != null)
                {
                    pizzaDaModificare.Nome = data.Pizza.Nome;
                    pizzaDaModificare.Descrizione = data.Pizza.Descrizione;
                    pizzaDaModificare.Image = data.Pizza.Image;
                    pizzaDaModificare.Prezzo = data.Pizza.Prezzo;
                    pizzaDaModificare.CategoriaId = data.Pizza.CategoriaId;
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

        public IActionResult DettaglioPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? dettaglioPizza = db.Pizze.Where(pizze => pizze.Id == id).Include(pizza => pizza.Catwgoria).FirstOrDefault();

                if (dettaglioPizza == null)
                {
                    return NotFound($"La pizza con id {id} non è stata trovata!");
                }
                else
                {
                    return View("DettaglioPizza", dettaglioPizza);
                }
            }
        }


    }




}
