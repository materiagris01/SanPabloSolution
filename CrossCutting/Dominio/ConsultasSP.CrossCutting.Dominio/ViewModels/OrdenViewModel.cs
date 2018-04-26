using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.ViewModels
{
    public class OrdenViewModel
    {
        public int NROTRA { get; set; }
        public string DESEMP { get; set; }
        public string DESSUC { get; set; }
        public string DESSEC { get; set; }
        public string DESCOM { get; set; }
        public string DESSUP { get; set; }
        public string DESPRV { get; set; }
        public string NRODOC { get; set; }
        public string DESCON { get; set; }
        public string MONEDA { get; set; }
        public decimal VALTOT { get; set; }
        public DateTime FECDOC { get; set; }
        public string ESTADO { get; set; }
        public string CICAPR { get; set; }
    }
    public class OrdenDetalleViewModel
    {
        public int NROTRA { get; set; }
        public int NROITE { get; set; }
        public int CODPRO { get; set; }
        public string DESPRO { get; set; }
        public string CODUNI { get; set; }
        public int CANPRO { get; set; }
        public decimal VALUNI { get; set; }
        public decimal VALTOT { get; set; }
        public string DESADI { get; set; }
    }
    public class OrdenAdjuntosViewModel
    {
        public int NROTRA { get; set; }
        public int NROITE { get; set; }
        public string DESADJ { get; set; }
        public string FORMAT { get; set; }
        public string CONTEN { get; set; }
    }

}
