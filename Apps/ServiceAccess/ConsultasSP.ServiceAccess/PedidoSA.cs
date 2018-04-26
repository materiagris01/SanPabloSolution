using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.ServiceAccess
{
    public class PedidoSA
    {
        public async Task<List<PedidosViewModel>> ObtenerPedidos(GetPedOrdBindingModel GetPedOrdBindingModel)
        {
            try
            {
                List<PedidosViewModel> Lista = new List<PedidosViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<GetPedOrdBindingModel, PedidosViewModel>(GetPedOrdBindingModel, UrlServicios.UrlPedido);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<PedidosViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<PedidosViewModel>();
            }
        }

        public async Task<List<PedidosDetalleViewModel>> ObtenerDetallePedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            try
            {
                List<PedidosDetalleViewModel> Lista = new List<PedidosDetalleViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<UsuarioTramiteBindingModel, PedidosDetalleViewModel>(UsuarioTramiteBindingModel, UrlServicios.UrlPedidoDetalle);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<PedidosDetalleViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<PedidosDetalleViewModel>();
            }
        }
        public async Task<List<PedidosAdjuntosViewModel>> ObtenerAdjuntosPedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            try
            {
                List<PedidosAdjuntosViewModel> Lista = new List<PedidosAdjuntosViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<UsuarioTramiteBindingModel, PedidosAdjuntosViewModel>(UsuarioTramiteBindingModel, UrlServicios.UrlAdjuntos);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<PedidosAdjuntosViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<PedidosAdjuntosViewModel>();
            }
        }

        public async Task<UsuarioTramiteViewModel> ActualizarPedidos(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            try
            {
                return await HttpClientService.Instance.PutResponse<UsuarioTramiteBindingModel, UsuarioTramiteViewModel>(UsuarioTramiteBindingModel, UrlServicios.UrlActualiza);
            }
            catch (Exception ex)
            {
                return new UsuarioTramiteViewModel();
            }
        }
    }
}
