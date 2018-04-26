using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using ConsultasSP.ServiceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.Core
{
    public class PreordenCore
    {
        public async Task<List<OrdenViewModel>> ObtenerPreorden(GetPedOrdBindingModel GetPedOrdBindingModel)
        {
            PreordenSA PreordenSA = new PreordenSA();
            return await PreordenSA.ObtenerPreorden(GetPedOrdBindingModel);
        }
        public async Task<List<OrdenDetalleViewModel>> ObtenerDetallePreorden(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PreordenSA PreordenSA = new PreordenSA();
            return await PreordenSA.ObtenerDetallePreorden(UsuarioTramiteBindingModel);
        }
        public async Task<List<OrdenAdjuntosViewModel>> ObtenerAdjuntosPreorden(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PreordenSA PreordenSA = new PreordenSA();
            return await PreordenSA.ObtenerAdjuntosPreorden(UsuarioTramiteBindingModel);
        }
        public async Task<UsuarioTramiteViewModel> ActualizarPreordenes(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PreordenSA PreordenSA = new PreordenSA();
            return await PreordenSA.ActualizarPreordenes(UsuarioTramiteBindingModel);
        }
    }
}
