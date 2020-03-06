using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Utils
{
    public class EmailUtil
    {
        public static void EnviarCorreoUsuarioNuevo(string clienteNombre, string nit, string email,
            string responsable, string direccion, string telefono, string username)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(ConfigurationSettings.AppSettings["emailUser"]);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Creacion de un Cliente Nuevo";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add(ConfigurationSettings.AppSettings["copiaCorreo"]); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "Un nuevo Cliente ha sido creado en la plataforma <br/><br/>";
            mmsg.Body += "Información del cliente:<br/>";
            mmsg.Body += "Nombre Cliente: " + clienteNombre + "<br/>";
            mmsg.Body += "NIT/NIP: " + nit + "<br/>";
            mmsg.Body += "Dirección: : " + direccion + "<br/>";
            mmsg.Body += "Responsable: " + responsable + "<br/>";
            mmsg.Body += "Telefono: " + telefono + "<br/>";
            mmsg.Body += "email: " + email + "<br/>";
            mmsg.Body += "Nombre de usuario: " + username + "<br/>";

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["emailUser"]);

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["emailUser"], Utils.Base64Decode(ConfigurationSettings.AppSettings["emailPass"]));

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = Convert.ToInt16(ConfigurationSettings.AppSettings["port"]);
            cliente.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSL"]);

            cliente.Host = ConfigurationSettings.AppSettings["host"]; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                Console.WriteLine(ex.ToString());
            }
        }

        public static void EnviarCorreoUsuarioNuevo(string clienteNombre, string email)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Bienvenido a CABUSoft Software de Inventarios";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("camilo_1.9@hotmail.com"); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "Bienvenido <strong>" + clienteNombre + "</strong> a Cabusoft <strong>Software de Inventarios</strong> <br/><br/>";
            mmsg.Body += "Su cuenta ha sido creada satisfactoriamente, pronto nos pondremos en contacto contigo para realizar el proceso de activacion de usuario.<br/><br/>";
            mmsg.Body += "Ahora cuenta con nuestro apoyo para que su negocio cresca.<br/>";


            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["emailUser"]);

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["emailUser"], Utils.Base64Decode(ConfigurationSettings.AppSettings["emailPass"]));

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = Convert.ToInt16(ConfigurationSettings.AppSettings["port"]);
            cliente.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSL"]);

            cliente.Host = ConfigurationSettings.AppSettings["host"]; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }

        public static void EnviarCorreoActivarUsuario(string clienteNombre, string email, string host)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Bienvenido a CABUSoft Software de Inventarios";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("camilo_1.9@hotmail.com"); //Opcional
            //Cuerpo del Mensaje
            mmsg.Body = "Bienvenido <strong>" + clienteNombre + "</strong> a Cabusoft <strong>Software de Inventarios</strong> <br/><br/>";
            mmsg.Body += "HA sido creada una nueva cuenta para Nuestro Software de inventarios.<br/>";
            mmsg.Body += "Para activar su usario ingrese al link http://"+host+"/ActivarUsuario.aspx?add="+Utils.Base64Encode(Utils.Base64Encode(Utils.Base64Encode(clienteNombre)))+".<br/>"+
                " Recuerde que este link solo estara disponible por 48 horas. luego de esto tendra que ponerse en contacto con el Administrador de la plataforma<br/><br/>";
            mmsg.Body += "Ahora cuenta con nuestro apoyo para que su negocio cresca.<br/>";


            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["emailUser"]);

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["emailUser"], Utils.Base64Decode(ConfigurationSettings.AppSettings["emailPass"]));

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = Convert.ToInt16(ConfigurationSettings.AppSettings["port"]);
            cliente.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSL"]);

            cliente.Host = ConfigurationSettings.AppSettings["host"]; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }

        public static void EnviarCorreoRestablecerContrasena(string email, string usernameEn, string password, string username)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Reestablecimiento de Usuario CabuSoft Inventarios";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("camilo_1.9@hotmail.com"); //Opcional
            //Cuerpo del Mensaje
            mmsg.Body = "Señor <strong>" + username + "</strong> Hemos Recibido una Solicitud de Reestablecimiento de Contraseña<br/><br/>";
            mmsg.Body += "Para Reestablecer Su contraseña debe ingresar al siguiente link: "
                + "http://xeropapel.com/gestionDocumental/Account/ReestablecerPassword.aspx?ui=" + usernameEn + "&ui2=" + password + "<br/><br/>";
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["emailUser"]);

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["emailUser"], Utils.Base64Decode(ConfigurationSettings.AppSettings["emailPass"]));

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = Convert.ToInt16(ConfigurationSettings.AppSettings["port"]);
            cliente.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSL"]);

            cliente.Host = ConfigurationSettings.AppSettings["host"]; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.Write("Error al enviar el correo: " + ex);
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }

        public static void EnviarCorreoActivarCliente(string email, string fechaIni, string fechaFin, string empresa)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Activación Usuario CabuSoft Inventarios";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("camilo_1.9@hotmail.com"); //Opcional
            //Cuerpo del Mensaje
            mmsg.Body = "Señores(as) <strong>" + empresa + "</strong> <br/><br/><br/>"
            + "Su cuenta fue activada exitosamente en nuestro Software de Inventarios<br/>";
            mmsg.Body += "Periodo Suscrito: " + fechaIni + " hasta el " + fechaFin;
            mmsg.Body += "<br/>Usted o su empresa ya puede, a través del siguiente enlace hacer uso de su herramienta archivística: http://xeropapel.com/gestionDocumental/Account/Login.aspx <br/><br/>";
            mmsg.Body += "Recuerde ingresar con el usuario y contraseña que usted mismo creó.";
            mmsg.Body += "<br/><br/><br/>CabuSoft.COM";

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["emailUser"]);

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["emailUser"], Utils.Base64Decode(ConfigurationSettings.AppSettings["emailPass"]));

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = Convert.ToInt16(ConfigurationSettings.AppSettings["port"]);
            cliente.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["SSL"]);

            cliente.Host = ConfigurationSettings.AppSettings["host"]; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.Write("Error al enviar el correo: " + ex);
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }
    }
}
