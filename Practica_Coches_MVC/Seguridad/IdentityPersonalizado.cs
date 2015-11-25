using System.Security.Principal;
using System.Web.Security;

namespace Practica_Coches_MVC.Seguridad
{//Aqui se puede guardart toda la información que se quiera del usuario.
    public class IdentityPersonalizado:IIdentity
    {
        public string Name
        {//El name debe ser algo único, que permita identificar unívocamente el usuario.
         //Por ejemplo el nif, un nick, un correo electrónico.
            get { return Login; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public int idUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public IIdentity Identity { get; set; }

        //Constructor
        public IdentityPersonalizado(IIdentity identity)
        {
            this.Identity = identity;
            var usuario = Membership.GetUser(identity.Name) as UsuarioMembership;
            idUsuario = usuario.idUsuario;
            Nombre = usuario.Nombre;
            Apellidos = usuario.Apellidos;
            Rol = usuario.Rol;
            Email = usuario.Email;
        }
    }
}

//providers se encargan del manejo de autorización en tema de roles, autenticación y autorización.
//Principal: es el contenedor que contiene todos los roles que tienes ademas el hecho decimal estar autenticado y 
//un objeto que mantiene tu identidad que contiene nombre, apellidos, etc. (los campos que uno quiera)
//principal representa que eres un usuario mientras que identitty representa qué usuario concreto eres 
//(y sus datos asociados)








