using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Practica_Coches_MVC.Seguridad;

namespace Practica_Coches_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        //Se crea un metodo para asegurar que httpcontext mantiene siempre un principal,
        //con un identity, de tipo personalizado.
        //sender=quien envia el evento
        //EventArgs e los argumentos que se pasan al evento/el delegado del evento 
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity = new IdentityPersonalizado(HttpContext.Current.User.Identity);
                var principal = new PrincipalPersonalizado(identity);
                HttpContext.Current.User = principal;
            }
        }
    }
}
