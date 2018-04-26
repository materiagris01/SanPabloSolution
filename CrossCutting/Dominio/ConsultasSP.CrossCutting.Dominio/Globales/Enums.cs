using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultasSP.CrossCutting.Dominio.Globales
{
    public static class LoginParameters
    {
        public const string Type = "grant_type";
        public const string TypePassword = "password";
        public const string Username = "username";
        public const string Password = "password";
    }

    public static class UrlServicios
    {
        public const string UrlPedido = "pedidos";
        public const string UrlPreOrden = "preordenes";
        public const string UrlPedidoDetalle = "pedido/detalles";
        public const string UrlPreOrdenDetalle = "preorden/detalles";
        public const string UrlAdjuntos = "tramite/adjuntos";
        public const string UrlLogin = "login";
        public const string UrlLogout = "preordenes";
        public const string UrlActualiza = "tramite/actualizar";

    }
}
