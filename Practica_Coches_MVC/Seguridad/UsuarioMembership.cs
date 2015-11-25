using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using Practica_Coches_MVC.Models;
using Practica_Coches_MVC.Utilidades;

namespace Practica_Coches_MVC.Seguridad
{//Se encarga de almacenar en memoria la información de Usuario que nos interesa.
    public class UsuarioMembership:MembershipUser
    {
        public int idUsuario { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public override string Email { get; set; }

        public UsuarioMembership(Usuario user)
        {
            idUsuario = user.idUsuario;
            Nombre = user.Nombre;
            Apellidos = user.Apellidos;
            Rol = user.Rol.Nombre;
            Login = user.Login;
            Email = user.Email;
        }


    }

    

}