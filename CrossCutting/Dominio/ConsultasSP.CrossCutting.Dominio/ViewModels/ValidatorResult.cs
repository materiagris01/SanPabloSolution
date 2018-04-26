using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.ViewModels
{
    public class ValidatorResult
    {
        public ValidatorResult(string MensajeError, string CampoError)
        {
            this.MensajeError = MensajeError;
            this.CampoError = CampoError;
        }
        public string MensajeError { get; set; }
        public string CampoError { get; set; }
    }
    public class ErrorMessage
    {
        public string Message { get; set; }
    }
    public class ErrorMessageLogin
    {
        public string error_description { get; set; }
    }
}
