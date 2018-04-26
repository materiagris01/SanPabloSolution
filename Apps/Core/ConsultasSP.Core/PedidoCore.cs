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
    public class PedidoCore
    {
        public async Task<List<PedidosViewModel>> ObtenerPedidos(GetPedOrdBindingModel GetPedOrdBindingModel)
        {
            PedidoSA PedidoSA = new PedidoSA();
            return await PedidoSA.ObtenerPedidos(GetPedOrdBindingModel);
        }
        public async Task<List<PedidosDetalleViewModel>> ObtenerDetallePedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PedidoSA PedidoSA = new PedidoSA();
            return await PedidoSA.ObtenerDetallePedidos(UsuarioTramiteBindingModel);
        }
        public async Task<List<PedidosAdjuntosViewModel>> ObtenerAdjuntosPedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PedidoSA PedidoSA = new PedidoSA();
            return await PedidoSA.ObtenerAdjuntosPedidos(UsuarioTramiteBindingModel);
        }
        public async Task<UsuarioTramiteViewModel> ActualizarPedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            PedidoSA PedidoSA = new PedidoSA();
            return await PedidoSA.ActualizarPedidos(UsuarioTramiteBindingModel);
        }
    }
}
