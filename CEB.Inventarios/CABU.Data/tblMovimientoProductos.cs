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
    
    public partial class tblMovimientoProductos
    {
        public int id_movimientoM { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> id_producto { get; set; }
        public Nullable<decimal> valor { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
    
        public virtual tblProductos tblProductos { get; set; }
    }
}