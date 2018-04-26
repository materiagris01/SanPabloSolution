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
    public class PreordenSA
    {
        public async Task<List<OrdenViewModel>> ObtenerPreorden(GetPedOrdBindingModel GetPedOrdBindingModel)
        {
            try
            {
                List<OrdenViewModel> Lista = new List<OrdenViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<GetPedOrdBindingModel, OrdenViewModel>(GetPedOrdBindingModel, UrlServicios.UrlPreOrden);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<OrdenViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<OrdenViewModel>();
            }
        }

        public async Task<List<OrdenDetalleViewModel>> ObtenerDetallePreorden(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            try
            {
                List<OrdenDetalleViewModel> Lista = new List<OrdenDetalleViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<UsuarioTramiteBindingModel, OrdenDetalleViewModel>(UsuarioTramiteBindingModel, UrlServicios.UrlPreOrdenDetalle);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<OrdenDetalleViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<OrdenDetalleViewModel>();
            }
        }
        public async Task<List<OrdenAdjuntosViewModel>> ObtenerAdjuntosPreorden(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
        {
            try
            {
                List<OrdenAdjuntosViewModel> Lista = new List<OrdenAdjuntosViewModel>();

                Lista = await HttpClientService.Instance.PostListResponse<UsuarioTramiteBindingModel, OrdenAdjuntosViewModel>(UsuarioTramiteBindingModel, UrlServicios.UrlAdjuntos);
                if (Lista.Count > 0)
                {
                    return Lista;
                }
                else
                {
                    return new List<OrdenAdjuntosViewModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<OrdenAdjuntosViewModel>();
            }
        }
        public async Task<UsuarioTramiteViewModel> ActualizarPreordenes(UsuarioTramiteBindingModel UsuarioTramiteBindingModel)
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
