using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminSucursales
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

        public int insertarSucursal(string nombre, string direccion, string telefono, string email, int idEmpresa)
        {
            tblSucursales sucursal = new tblSucursales();
            sucursal.nombre = nombre;
            sucursal.direccion = direccion;
            sucursal.email = email;
            sucursal.id_empresa = idEmpresa;
            sucursal.telefono = telefono;
            DbContext.tblSucursales.Add(sucursal);
            return DbContext.SaveChanges();
        }

        public tblSucursales obtenerSucursalById(int idSucursal)
        {
            return DbContext.tblSucursales.FirstOrDefault(p=>p.id_sucursal==idSucursal);
        }
    }
}
