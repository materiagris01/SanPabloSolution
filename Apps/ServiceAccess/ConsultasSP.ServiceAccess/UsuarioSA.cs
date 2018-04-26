using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsultasSP.ServiceAccess
{
    public class UsuarioSA
    {
        public async Task<DatosUsuarioViewModel> LoginUsuario(UsuarioViewModel UsuarioViewModel)
        {

            try
            {
                DatosUsuarioViewModel DatosUsuarioViewModel = new DatosUsuarioViewModel();

                DatosUsuarioViewModel = await HttpClientService.Instance.PostResponseLogin<UsuarioViewModel, DatosUsuarioViewModel>(UsuarioViewModel, UrlServicios.UrlLogin);
                return DatosUsuarioViewModel;

            }
            catch (Exception ex)
            {
                return new DatosUsuarioViewModel();
            }
        }
    }
}
