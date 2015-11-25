using System.Security.Principal;

namespace Practica_Coches_MVC.Seguridad
{
    public class PrincipalPersonalizado:IPrincipal
    {
        public bool IsInRole(string role)
        {
            return MiIdentityPersonalizado.Rol == role;
        }
        //Con polimorfismo y al definir private set, hacemos que IIdentity almacene datos
        //forzando que IIdentity sea IdentityPersonalizado
        public IIdentity Identity { get; private set; }

        public IdentityPersonalizado MiIdentityPersonalizado
        {
            get { return (IdentityPersonalizado) Identity; }
            set { Identity = value; }
        }

        //Constructor
        public PrincipalPersonalizado(IdentityPersonalizado identity)
        {
            Identity = identity;
        }

    }
}
