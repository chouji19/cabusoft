using CABU.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminUsuarios
    {

        private CABUConection context;

        public CABUConection DbContext
        {
            get
            {
                if (context == null)

                    context = new CABUConection();
                return context;

            }
        }

        public List<tblPermisosUsuarios> obtenerPermisosUsuarios(int idUsuario)
        {
            return (from p in DbContext.tblPermisosUsuarios
                    where p.id_usuario == idUsuario
                    select p).ToList();
        }

        public List<tblPermisos> obtenerPermisos()
        {
            return (from p in DbContext.tblPermisos
                    select p).ToList();
        }

        public int insertarEmpresa(string nombre, string tipoId, string identificacion, string direccion,
            string contacto, string telefono, string celular, string email)
        {
            tblEmpresa empresa = new tblEmpresa();
            empresa.nombre = nombre;
            empresa.tipo_identificacion = Convert.ToInt32(tipoId);
            empresa.identificacion = identificacion;
            empresa.direccion = direccion;
            empresa.contacto_nombre = contacto;
            empresa.contacto_telefono = telefono;
            empresa.contacto_celular = celular;
            empresa.contacto_email = email;
            empresa.activa = false;
            DbContext.tblEmpresa.Add(empresa);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(empresa).GetDatabaseValues();
                return empresa.id_empresa;
            }
            return res;
        }

        public int insertarSucursalPrincipal(int idEmpresa)
        {
            tblEmpresa empresa = DbContext.tblEmpresa.FirstOrDefault(p => p.id_empresa == idEmpresa);
            tblSucursales sucursal = new tblSucursales();
            sucursal.direccion = empresa.direccion;
            sucursal.email = empresa.contacto_email;
            sucursal.nombre = empresa.nombre;
            sucursal.id_empresa = idEmpresa;
            sucursal.telefono = empresa.contacto_telefono;
            DbContext.tblSucursales.Add(sucursal);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(sucursal).GetDatabaseValues();
                return sucursal.id_sucursal;
            }
            return res;
        }

        public int insertarUsuario(string identificacion, string nombre, string apellido, string celular, string password,
            int rol, int idSucursal, string username, string correo)
        {
            tblUsuarios user = new tblUsuarios();
            user.identificacion = identificacion;
            user.nombres = nombre;
            user.apellidos = apellido;
            user.celular = celular;
            user.password = Utils.Utils.Base64Encode(password);
            user.id_rol = rol;
            user.id_sucursal = idSucursal;
            user.username = username;
            user.activo = false;
            user.fecha_ingreso = DateTime.Now;
            user.email = correo;
            DbContext.tblUsuarios.Add(user);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(user).GetDatabaseValues();
                enviarCorreoUsuarioNuevo(user);
                return user.id_usuario;
            }
            return res;
        }

        public int insertarUsuarioActivacion(string identificacion, string nombre, string apellido, string celular,
            int rol, int idSucursal, string username, string correo, string host)
        {
            tblUsuarios user = new tblUsuarios();
            user.identificacion = identificacion;
            user.nombres = nombre;
            user.apellidos = apellido;
            user.celular = celular;
            //user.password = Utils.Utils.Base64Encode(password);
            user.id_rol = rol;
            user.id_sucursal = idSucursal;
            user.username = username;
            user.activo = false;
            user.fecha_ingreso = DateTime.Now;
            user.email = correo;
            DbContext.tblUsuarios.Add(user);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(user).GetDatabaseValues();
                enviarCorreoActivarUsuario(user, host);
                return user.id_usuario;
            }
            return res;
        }

        public void enviarCorreoUsuarioNuevo(tblUsuarios user)
        {
            EmailUtil.EnviarCorreoUsuarioNuevo(user.username, user.email);
            tblEmpresa empresa = DbContext.tblEmpresa.FirstOrDefault(p => p.id_empresa == user.tblSucursales.id_empresa);
            EmailUtil.EnviarCorreoUsuarioNuevo(empresa.nombre, empresa.identificacion, empresa.contacto_email,
                empresa.contacto_nombre, empresa.direccion, empresa.contacto_telefono, user.username);
        }

        public void enviarCorreoActivarUsuario(tblUsuarios user, string host)
        {
            EmailUtil.EnviarCorreoActivarUsuario(user.username, user.email, host);
            //tblEmpresa empresa = DbContext.tblEmpresa.FirstOrDefault(p => p.id_empresa == user.tblSucursales.id_empresa);
            //EmailUtil.EnviarCorreoUsuarioNuevo(empresa.nombre, empresa.identificacion, empresa.contacto_email,
            //    empresa.contacto_nombre, empresa.direccion, empresa.contacto_telefono, user.username);
        }

        public bool validarUsuarioEmpresa(string usuario)
        {
            tblUsuarios user = DbContext.tblUsuarios.FirstOrDefault(p => p.username.Equals(usuario));
            if (user == null)
                return true;
            else
                return false;
        }

        public bool ValidatePassword(string p)
        {
            return Utils.Utils.ValidatePassword(p);
        }

        public tblUsuarios obtenerUsuarioEncriptado(string usuario)
        {
            Utils.Utils util = new Utils.Utils();
            string username = Utils.Utils.Base64Decode(Utils.Utils.Base64Decode(Utils.Utils.Base64Decode(usuario)));
            return DbContext.tblUsuarios.FirstOrDefault(p => p.username.Equals(username));
        }

        public string obtenerUsername(string usuario)
        {
            return Utils.Utils.Base64Decode(Utils.Utils.Base64Decode(Utils.Utils.Base64Decode(usuario)));
        }

        public void actualizarPasswordActivacion(int idUsuario, string password)
        {
            tblUsuarios usuario = DbContext.tblUsuarios.FirstOrDefault(p => p.id_usuario == idUsuario);
            usuario.password = Utils.Utils.Base64Encode(password);
            usuario.activo = true;
            DbContext.SaveChanges();
        }

        public void limpiarPermisos(int idUsuario)
        {
            List<tblPermisosUsuarios> subc = (from p in DbContext.tblPermisosUsuarios
                                              where p.id_usuario == idUsuario
                                              select p).ToList();
            foreach (tblPermisosUsuarios item in subc)
                DbContext.tblPermisosUsuarios.Remove(item);
            DbContext.SaveChanges();
        }

        public void agregarPermiso(int idUsuario, string permiso)
        {
            tblPermisosUsuarios permisos = new tblPermisosUsuarios();
            permisos.id_permiso = Convert.ToInt16(permiso);
            permisos.id_usuario = idUsuario;
            DbContext.tblPermisosUsuarios.Add(permisos);
            DbContext.SaveChanges();
        }

        public tblUsuarios obtenerUsuarioById(int idUsuario)
        {
            return DbContext.tblUsuarios.FirstOrDefault(p=>p.id_usuario == idUsuario);
        }
    }
}
