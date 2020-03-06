using CABU.Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminMovimientos
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

        public List<tblMovimientos> insertarMovimiento(string fecha, string tipo, string descripcion, string valor, int idSucursal)
        {
            DateTime fechaMvt = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            tblMovimientos mov = new tblMovimientos();
            if (descripcion != "" && fecha != "" && tipo != "" && valor != "")
            {
                mov.descripcion = descripcion;
                mov.fecha = fechaMvt;
                mov.fecha_ingreso = DateTime.Now;
                mov.tipo = tipo;
                mov.valor = Convert.ToDecimal(valor);
                mov.id_sucursal = idSucursal;
                DbContext.tblMovimientos.Add(mov);
                DbContext.SaveChanges();
                return (from p in DbContext.tblMovimientos
                        select p).OrderByDescending(q => q.fecha).Take(20).ToList();
            }
            return null;
        }

        public List<tblMovimientos> consultarMovimientos(string fecha, string tipo, string descripcion, string valor, int idSucursal) {

            List<tblMovimientos> mov = (from p in DbContext.tblMovimientos
                                        where p.id_sucursal == idSucursal
                                        select p).OrderByDescending(q=>q.fecha).ToList();
            if (fecha != "")
            {
                DateTime fechaMvt = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                mov = mov.Where(p=>p.fecha==fechaMvt).ToList();
            }
            if (tipo != "")
            {
                mov = mov.Where(p => p.tipo.Contains(tipo)).ToList();
            }
            if (descripcion != "")
                mov = mov.Where(p => p.descripcion.Contains(descripcion)).ToList(); 

            return mov;
        }

        public List<string> obtenerMovimientosByEmpresa(int? nullable)
        {
            List<string> lista = (from p in DbContext.tblMovimientos
                                  select p.descripcion + "                    ".Substring(0, 20).Trim()).Distinct().ToList();
            return lista;
        }

        public System.Data.DataTable obtenerIngresosGastos(string desde, string hasta, int idSucursal)
        {
            DateTime ini = DateTime.ParseExact(desde, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime fin = DateTime.ParseExact(hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            fin = fin.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<vIngresosGastos> movs = (from p in DbContext.vIngresosGastos
                                         where p.fecha>=ini && p.fecha <= fin && p.id_sucursal == idSucursal select p).ToList();
            if (movs != null)
                return ConvertDataTable.ConvertToDataTable(movs);
            return null;
        }
    }
}
