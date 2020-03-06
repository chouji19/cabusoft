using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class InicioSesion
    {
        private CABUConection dbContext;

        public CABUConection DbContext
        {
            get
            {
                if (dbContext == null)

                    dbContext = new CABUConection();
                return dbContext;

            }
        }
        public tblUsuarios iniciarSesion(String usuario, String password)
        {
            string pass = Utils.Utils.Base64Encode(password);
            tblUsuarios user = DbContext.tblUsuarios.FirstOrDefault(p=>p.username.ToLower().Equals(usuario.ToLower())
            && p.password.ToLower().Equals(pass));
            if (user != null && user.id_usuario != 0)
            {
                return user;
            }
            else
                return null;
        }

        public bool validarPermiso(int idUsuario,string permiso) {
            int idPermiso = DbContext.tblPermisos.FirstOrDefault(p => p.valor.Equals(permiso)).id_permiso;
            tblPermisosUsuarios permisos = DbContext.tblPermisosUsuarios.FirstOrDefault(p=>p.id_usuario==idUsuario && p.id_permiso==idPermiso);
            if (permisos != null)
            {
                return true;
            }
            return false;
        }
    }
}
