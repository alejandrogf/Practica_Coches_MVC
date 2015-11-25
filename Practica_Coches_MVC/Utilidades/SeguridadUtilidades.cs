using System;
using System.Security.Cryptography;
using System.Text;

namespace Practica_Coches_MVC.Utilidades
{
    public class SeguridadUtilidades
    {
        //Para obtener el SHA1 de un texto, en este caso la contraseña.
        public static string GetSha1(string texto)
        {
            //Creamos el codificador del SHA1, el generador.
            var sha = SHA1.Create();
            //Se crea un codificador (ascii o utf-8) para los caracteres
            //de la contraseña
            UTF8Encoding encoding=new UTF8Encoding();
            //Array de bytes porque el sha devuelve eso.
            byte[] datosBytes;
            //Es un concatenador para cadenas de caracteres
            StringBuilder builder=new StringBuilder();
            //sha.computehash hace el cálculo para crear el sha.
            //Se le pasa el array de bytes que contiene los bytes de la contraseña
            //Devuelve un array de bytes con el codigo del sha1
            datosBytes = sha.ComputeHash(encoding.GetBytes(texto));

            for (int i = 0; i < datosBytes.Length; i++)
            {
                //{0:x2}dos digitos por byte en exadecimanl.
                builder.AppendFormat("{0:x2}", datosBytes[i]);
            }
            return builder.ToString();
        }
        //Construye un array de bytes con el contenido de la key que tenemos definida fija
        //null indica que no hay un numero para hacer saltos aleatorios (así la transformación
        //es siempre fija.
        //Getbytes32 para indicar que rellene hasta alcanzar 32 bytes
        public static byte[] GetKey(string txt)
        {
            return new PasswordDeriveBytes(txt, null).GetBytes(32);
        }

        public static string Cifrar(string contenido, string clave)
        {
            var encoding=new UTF8Encoding();
            //cifrado asimetrico: se usa cuando se quiera compartir con otros, cuando vaya a viajar 
            //y/o tenga que cifrar o descifrar con otra persona (en un trabajo en equipo)
            //cifrado simetrico: yo mismo conmigo mismo.
            var cripto=new RijndaelManaged();
            
            byte[] cifrado;
            byte[] retorno;
            //la key es un valor fijo, que se define (por ej en el webconfig )
            //y de una clave alfanumerica obtenemos su transformación en un array de bytes
            var key = GetKey(clave);
            //La clave se crea con la key que contiene el array de datos
            cripto.Key = key;
            //El IV es a partir de lo que genera la encriptaciópn. Es un array de bytes
            cripto.GenerateIV();
            byte[] aEncriptar = encoding.GetBytes(contenido);
            //Crea toda la encriptación desde la posición 0 hasta toda la longitud.
            cifrado = cripto.CreateEncryptor().TransformFinalBlock(aEncriptar, 0, aEncriptar.Length);
            //El tamaño del array de retorno es la suma del tamaño de IV + tamaño del cifrado
            retorno=new byte[cripto.IV.Length + cifrado.Length];
            //Estas dos lineas son para copiar el contenido de IV y cifrado.
            //CopyTo tiene dos argumentos, que son qué copiar y desde que posición
            cripto.IV.CopyTo(retorno,0);
            cifrado.CopyTo(retorno,cripto.IV.Length);
            //Se devuelve el array de bytes de cifrado en forma de cadena de texto aunque se puede
            //dejar en bytes para guardarlo en la base de datos
            return Convert.ToBase64String(retorno);
        }

        public static string Descifrar(byte[] contenido, string clave)
        {
            UTF8Encoding encoding=new UTF8Encoding();
            var cripto = new RijndaelManaged();
            //El iv temporal está vacio, solo está definido su tamaño según el IV por defecto ya que el IV de
            //rijndael siemrpe tiene el mismo tamaño.
            var ivTemporal = new byte[cripto.IV.Length];
            
            //la key es un valor fijo, que se define (por ej en el webconfig )
            //y de una clave alfanumerica obtenemos su transformación en un array de bytes
            var key = GetKey(clave);
            //Cifrado es mas pequeño que datos (que es  el contenido cifrado en la base de datos) porque
            //es la parte sin el IV
            var cifrado = new byte[contenido.Length - ivTemporal.Length];

            cripto.Key = key;
            //Copio el trozo del IV a un lado y el cifrado a otro.
            Array.Copy(contenido,ivTemporal,ivTemporal.Length);
            //Copia de datos, a partir de, donde copias, desde q posición y cuanto copias.
            Array.Copy(contenido,ivTemporal.Length,cifrado,0,cifrado.Length);

            cripto.IV = ivTemporal;
            var descifrado = cripto.CreateDecryptor().TransformFinalBlock(cifrado, 0, cifrado.Length);

            return Convert.ToBase64String(descifrado);
        }

    }
}
