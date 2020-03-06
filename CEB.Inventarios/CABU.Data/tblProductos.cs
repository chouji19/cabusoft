//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CABU.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProductos
    {
        public tblProductos()
        {
            this.tblMovimientoProductos = new HashSet<tblMovimientoProductos>();
            this.tblVentas = new HashSet<tblVentas>();
        }
    
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int existencias { get; set; }
        public Nullable<bool> servicio { get; set; }
        public string tipo { get; set; }
        public string marca { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<System.DateTime> last_update { get; set; }
        public Nullable<int> id_subcategoria { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<decimal> valor_compra { get; set; }
        public string imagen { get; set; }
        public Nullable<int> id_sucursal { get; set; }
    
        public virtual tblCategorias tblCategorias { get; set; }
        public virtual tblEstadosProductos tblEstadosProductos { get; set; }
        public virtual ICollection<tblMovimientoProductos> tblMovimientoProductos { get; set; }
        public virtual tblSubCategorias tblSubCategorias { get; set; }
        public virtual tblSucursales tblSucursales { get; set; }
        public virtual ICollection<tblVentas> tblVentas { get; set; }
    }
}