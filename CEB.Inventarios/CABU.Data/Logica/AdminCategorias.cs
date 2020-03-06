using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminCategorias
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

        public tblCategorias obtenerCategoriaById(int idCategoria)
        {
            tblCategorias categoria = DbContext.tblCategorias.FirstOrDefault(p => p.id_categoria == idCategoria);
            return categoria;
        }

        public tblSubCategorias obtenerSubCategoriaById(int id_subcategoria)
        {
            tblSubCategorias categoria = DbContext.tblSubCategorias.FirstOrDefault(p => p.id_subcategoria == id_subcategoria);
            return categoria;
        }


        public List<tblSubCategorias> obtenerSubCategoriasByCategoria(int idCategoria)
        {
            return (from p in DbContext.tblSubCategorias
                                                   where p.id_categoria == idCategoria
                                                   select p).ToList();
            
        }

        public int crearCategoria(string nombre, int idEmpresa)
        {
            tblCategorias cat = new tblCategorias();
            cat.nombre = nombre;
            cat.id_empresa = idEmpresa;
            DbContext.tblCategorias.Add(cat);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(cat).GetDatabaseValues();
                return cat.id_categoria;
            }
           
            return res; 

        }

        public int actualizarCategoria(int idCategoria, string nombre)
        {
            tblCategorias cat = DbContext.tblCategorias.FirstOrDefault(p=>p.id_categoria==idCategoria);
            if (cat != null)
            {
                cat.nombre = nombre;
            }
            int res= DbContext.SaveChanges();
            if (res > 0)
            {
                return cat.id_categoria;
            }
            return res;
        }

        //-2: tiene productos asociados y nos e peude eliminar
        public int eliminarCategoria(int idCategoria)
        {
            tblCategorias cat = DbContext.tblCategorias.FirstOrDefault(p => p.id_categoria == idCategoria);
            if (cat != null)
            {
                int productos = DbContext.tblProductos.Count(p=>p.id_categoria==cat.id_categoria);
                if (productos > 0)
                    return -2;
                else
                {
                    List<tblSubCategorias> subc = (from p in DbContext.tblSubCategorias
                                                   where p.id_categoria == cat.id_categoria
                                                   select p).ToList();
                    foreach (tblSubCategorias item in subc)
                        DbContext.tblSubCategorias.Remove(item);
                    DbContext.tblCategorias.Remove(cat);
                }
            }
            
            
            return DbContext.SaveChanges();
        }


        public int crearSubCategoria(string nombre, int idCategoria)
        {
            tblSubCategorias cat = new tblSubCategorias();
            cat.nombre = nombre;
            cat.id_categoria = idCategoria;
            DbContext.tblSubCategorias.Add(cat);
            int res = DbContext.SaveChanges();
            
            if (res > 0)
            {
                DbContext.Entry(cat).GetDatabaseValues();
                return cat.id_subcategoria;
            }
            return res;
        }

        public int actualizarSubCategoria(int idCSubategoria, string nombre)
        {
            tblSubCategorias cat = DbContext.tblSubCategorias.FirstOrDefault(p => p.id_subcategoria == idCSubategoria);
            if (cat != null)
            {
                cat.nombre = nombre;
            }
            return DbContext.SaveChanges();
        }

        public int eliminarSubCategoria(int idSubCategoria)
        {
            tblSubCategorias cat = DbContext.tblSubCategorias.FirstOrDefault(p => p.id_subcategoria == idSubCategoria);
            if (cat != null)
            {
                int productos = DbContext.tblProductos.Count(p => p.id_subcategoria == cat.id_subcategoria);
                if (productos > 0)
                    return -2;
                else
                {
                    DbContext.tblSubCategorias.Remove(cat);
                }
            }
            

            return DbContext.SaveChanges();
        }
    }
}
