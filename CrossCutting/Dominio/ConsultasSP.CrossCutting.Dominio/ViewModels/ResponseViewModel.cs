using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.ViewModels
{
    public class ResponseViewModel
    {

    }
    public class GetPedOrdViewModel
    {
        public string CODUSR { get; set; }
        public string NROTKN { get; set; }
        public string SYSORI { get; set; }
    }

    public class UsuarioTramiteViewModel
    {
        public Usuario Usuario { get; set; }
        public Tramite Tramite { get; set; }
    }

    public class Usuario
    {
        public string CODUSR { get; set; }
        public string NROTKN { get; set; }
    }

    public class Tramite
    {
        public int NROTRA { get; set; }
        public string ESTADO { get; set; }
        public string DESOBS { get; set; }
    }
}
