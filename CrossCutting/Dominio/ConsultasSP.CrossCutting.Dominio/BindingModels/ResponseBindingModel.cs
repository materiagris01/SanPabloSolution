using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.BindingModels
{
    public class ResponseBindingModel
    {
    }
    public class GetPedOrdBindingModel
    {
        public string CODUSR { get; set; }
        public string NROTKN { get; set; }
        public string SYSORI { get; set; }
    }

    public class UsuarioTramiteBindingModel
    {
        public UsuarioBindingModel Usuario { get; set; }
        public TramiteBindingModel Tramite { get; set; }
    }

    public class UsuarioBindingModel
    {
        public string CODUSR { get; set; }
        public string NROTKN { get; set; }
    }

    public class TramiteBindingModel
    {
        public int NROTRA { get; set; }
        public string ESTADO { get; set; }
        public string DESOBS { get; set; }
    }
}
