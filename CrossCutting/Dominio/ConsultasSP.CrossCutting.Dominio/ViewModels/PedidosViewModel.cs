using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.ViewModels
{
    public class PedidosViewModel
    {
        public int NROTRA { get; set; }
        public string DESEMP { get; set; }
        public string DESSUC { get; set; }
        public string DESSEC { get; set; }
        public string DESSOL { get; set; }
        public string NROSER { get; set; }
        public string NRODOC { get; set; }
        public string DESOBS { get; set; }
        public int CANITE { get; set; }
        public DateTime FECDOC { get; set; }
        public string ESTADO { get; set; }
    }

    public class PedidosDetalleViewModel
    {
        public int NROTRA { get; set; }
        public int NROITE { get; set; }
        public int CODPRO { get; set; }
        public string DESPRO { get; set; }
        public string CODUNI { get; set; }
        public int CANPRO { get; set; }
        public string DESADI { get; set; }
    }

    public class PedidosAdjuntosViewModel
    {
        public int NROTRA { get; set; }
        public int NROITE { get; set; }
        public string DESADJ { get; set; }
        public string FORMAT { get; set; }
        public string CONTEN { get; set; }
    }

    
   
}