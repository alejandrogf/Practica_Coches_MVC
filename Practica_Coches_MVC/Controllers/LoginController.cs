using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Practica_Coches_MVC.Models;
using Practica_Coches_MVC.Utilidades;

namespace Practica_Coches_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario modeloUsuario)
        {   //Este membership es el que tenemos definido en el webconfig. 
            //Asi que si queremos cambiar la forma de validar, se puede hacer creando otro membership y 
            //con solo cambiarlo en el webconfig e indicarlo aquí ya funciona.        
            if (Membership.ValidateUser(modeloUsuario.Login,modeloUsuario.Password))
            {
                //Session, TempData y [falta nombre] se acceden igual.
                
                Session["horaLogin"] = DateTime.Now;
                FormsAuthentication.RedirectFromLoginPage(modeloUsuario.Login,false);
                return null;
            }
            return View();
        }

        public ActionResult LogOff()
        {   //Limpia los datos.
            Session.Clear();
            //Cierra/elimina la sesión
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}