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
    
    public partial class tblEmpresa
    {
        public tblEmpresa()
        {
            this.tblSucursales = new HashSet<tblSucursales>();
            this.tblCategorias = new HashSet<tblCategorias>();
        }
    
        public int id_empresa { get; set; }
        public string nombre { get; set; }
        public int tipo_identificacion { get; set; }
        public string identificacion { get; set; }
        public string direccion { get; set; }
        public string contacto_nombre { get; set; }
        public string contacto_telefono { get; set; }
        public string contacto_celular { get; set; }
        public string contacto_email { get; set; }
        public Nullable<bool> activa { get; set; }
    
        public virtual ICollection<tblSucursales> tblSucursales { get; set; }
        public virtual ICollection<tblCategorias> tblCategorias { get; set; }
    }
}
