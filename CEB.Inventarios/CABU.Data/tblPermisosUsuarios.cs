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
    
    public partial class tblPermisosUsuarios
    {
        public int id_permiso { get; set; }
        public int id_usuario { get; set; }
        public int id_Permisos_usuarios { get; set; }
    
        public virtual tblPermisos tblPermisos { get; set; }
        public virtual tblUsuarios tblUsuarios { get; set; }
    }
}
