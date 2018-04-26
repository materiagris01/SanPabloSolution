using ConsultasSP.CrossCutting.Dominio.ViewModels;
using ConsultasSP.ServiceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.Core
{
    public class UsuarioCore
    {
        public async Task<DatosUsuarioViewModel> LoginUsuario(UsuarioViewModel UsuarioViewModel)
        {
            UsuarioSA UsuarioSA = new UsuarioSA();
            return await UsuarioSA.LoginUsuario(UsuarioViewModel);
        }
    }
}
