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
    
    public partial class tblRoles
    {
        public tblRoles()
        {
            this.tblUsuarios = new HashSet<tblUsuarios>();
        }
    
        public int id_rol { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<tblUsuarios> tblUsuarios { get; set; }
    }
}
