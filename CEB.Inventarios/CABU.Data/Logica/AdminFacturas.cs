using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminFacturas
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

        public List<tblFacturas> obtenerFacturasPorFechaYConcidencia(string ini, string fin, string filtro, int idSucursal)
        { 
            DateTime fechaIni;
            List<tblFacturas> facturas;
            if (!ini.Equals("") && !fin.Equals(""))
            {
                bool valido = DateTime.TryParseExact(ini, "dd/MM/yyyy",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out fechaIni);
                if (valido)
                {
                    DateTime fechaFin;
                    valido = DateTime.TryParseExact(fin, "dd/MM/yyyy",
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out fechaFin);
                    if (valido)
                    {

                        fechaFin = fechaFin.AddHours(23);
                        fechaFin = fechaFin.AddMinutes(59);
                        fechaFin = fechaFin.AddSeconds(59);
                        

                        if (filtro != "")
                        {
                            facturas = (from p in DbContext.tblFacturas
                                        where (p.fecha >= fechaIni && p.fecha <= fechaFin)
                                        && p.id_sucursal == idSucursal && (p.direccion.Contains(filtro) || p.celular.Contains(filtro)
                                        || p.nombre.Contains(filtro))
                                        select p).ToList();
                        }
                        else
                        {
                            facturas = (from p in DbContext.tblFacturas
                                        where (p.fecha >= fechaIni && p.fecha <= fechaFin && p.id_sucursal == idSucursal)                                      
                                        select p).ToList();
                        }
                        if (facturas == null || facturas.Count==0)
                        {
                            facturas = (from p in DbContext.tblFacturas
                                        join q in DbContext.tblVentas
                                        on p.id_factura equals q.id_factura
                                        where (q.tblProductos.nombre.Contains(filtro) ||
                                        q.tblProductos.descripcion.Contains(filtro)) && p.id_sucursal == idSucursal
                                        select p).ToList();
                        }
                        return facturas;
                    }
                }
            }
            else
            {
                facturas = (from p in DbContext.tblFacturas
                            where (p.direccion.Contains(filtro) || p.celular.Contains(filtro)
                            || p.nombre.Contains(filtro))
                            select p).ToList();
                if (facturas == null || facturas.Count == 0)
                {
                    facturas = (from p in DbContext.tblFacturas
                                join q in DbContext.tblVentas
                                on p.id_factura equals q.id_factura
                                where (q.tblProductos.nombre.Contains(filtro) ||
                                q.tblProductos.descripcion.Contains(filtro))
                                select p).ToList();
                }
                return facturas;
            }
            return null;
        }


        public tblFacturas obtenerFacturaByID(int idFactura)
        {
            return DbContext.tblFacturas.FirstOrDefault(p=> p.id_factura==idFactura);
        }

        public List<tblVentas> obtenerVentasByFactura(int idFactura)
        {
            List<tblVentas> ventas = (from p in DbContext.tblVentas
                                      where p.id_factura == idFactura
                                      select p).ToList();
            return ventas;
        }
    }
}
