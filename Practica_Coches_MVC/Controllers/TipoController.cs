using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica_Coches_MVC.Models;

namespace Practica_Coches_MVC.Controllers
{
    public class TipoController : Controller
    {
        //Conexión a la BD
        ParqueMotorEntities db = new ParqueMotorEntities();


        // GET: Tipo
        public ActionResult Index()
        {
            var data = db.Tipo;
            return View(data);
        }

        // GET: Producto
        public ActionResult Alta()
        {
            return View(new Tipo());
        }

        [HttpPost]
        public ActionResult Alta(Tipo modeloTipo)
        {

            if (ModelState.IsValid)
            {
                db.Tipo.Add(modeloTipo);
                db.SaveChanges();
                ModelState.Clear();
                return View(new Tipo());
            }

            return View(modeloTipo);
        }

        //Si no se usa AJAX, el modificar se hace en dos pasos. Por eso hay dos instrucciones.
        public ActionResult EditarTipo(int idTipo)
        {
            var data = db.Tipo.Find(idTipo);
            return View(data);
        }

        [HttpPost]
        //Esta linea es para añadir un sistema de seguridad (tipo captcha pero invisible)
        [ValidateAntiForgeryToken]
        public ActionResult EditarTipo(Tipo objetoTipo)
        {
            if (ModelState.IsValid)
            {
                //Al indicar que el estado del objeto es modificado, el programa por si solo
                //ya sabe que hacer, buscando el objeto que coincida con los datos que tiene
                //y actualiza el solo. Otra opción sería actualizar a mano, campo a campo.
                //En este caso no hay problema porque es poco, pero con una tabla de 200 campos
                //es un horror.
                db.Entry(objetoTipo).State = EntityState.Modified;
                db.SaveChanges();
                //Con esto se redirige a la acción que queremos que se lleve a cabo.
                return RedirectToAction("Index");
            }
            return View(objetoTipo);
        }

        public ActionResult VerVehiculos(int id)
        {
            ViewBag.vehiculo = db.Vehiculo.Where(o=>o.Tipo==id).ToList();
            
            ViewData["idTipo"] = id;
            ViewData["nombreTipo"] = db.Tipo.Find(id).Nombre;
            return  View();
        }

        [HttpPost]
        public ActionResult NuevoVehiculo(Vehiculo modeloVehiculo)
        {
            db.Vehiculo.Add(modeloVehiculo);
            db.SaveChanges();
            return Json(modeloVehiculo);
        }

        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Buscar(int idTipo, string txtBusqueda, string campoBusqueda)
        {
            var vehiculo = db.Vehiculo.Where(o => o.Tipo==idTipo);

            switch (campoBusqueda)
            {
                case "Matricula":
                    vehiculo = vehiculo.Where(o => o.Matricula.Contains(txtBusqueda));
                    break;
                case "Marca":
                    vehiculo = vehiculo.Where(o => o.Marca.Contains(txtBusqueda));
                    break;
                case "Modelo":
                    vehiculo = vehiculo.Where(o => o.Modelo.Contains(txtBusqueda));
                    break;
            };

            return PartialView("_listadoVehiculo", vehiculo.ToList());
        }


    }
}