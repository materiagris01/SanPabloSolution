using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.ViewModels
{
    public class UsuarioViewModel
    {
        public string CODUSR { get; set; }
        public string CLAUSR { get; set; }
    }
    public class LogoutViewModel
    {
        public string CODUSR { get; set; }
        public string NROTOK { get; set; }
    }
    public class DatosUsuarioViewModel
    {
        public string CODUSR { get; set; }
        public string CLAUSR { get; set; }
        public string NROTOK { get; set; }
        public string SYSORI { get; set; }
        public string USUARIO { get; set; }
        public string CARGO { get; set; }
        public int ROL { get; set; }
    }
}
