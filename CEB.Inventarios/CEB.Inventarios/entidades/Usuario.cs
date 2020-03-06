using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEB.Inventarios.entidades
{
    class Usuario
    {
        private string idUsuario;

        public string IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        private string identificacion;

        public string Identificacion
        {
            get { return identificacion; }
            set { identificacion = value; }
        }
        private string nombres;

        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }
        private string apellidos;

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }
        private string celular;

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private int rol;

        public int Rol
        {
            get { return rol; }
            set { rol = value; }
        }

        private bool activo;

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        
    }
}
