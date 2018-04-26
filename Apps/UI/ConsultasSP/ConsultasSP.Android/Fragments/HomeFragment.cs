using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ConsultasSP.Core;
using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using ConsultasSP.Droid.Activities.Pedidos;
using ConsultasSP.Droid.Activities.PreOrden;
using ConsultasSP.Util.Commom;
using System;
using System.Collections.Generic;

namespace ConsultasSP.Droid.Fragments
{
    public class HomeFragment : Fragment
    {
        RelativeLayout MenuPedido, MenuPreOrden;
        ImageView imgPedidos, imgPreorden;
        TextView txtCantidadPedidos, txtCantidadPreOrden;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static HomeFragment NewInstance()
        {
            var frag1 = new HomeFragment { Arguments = new Bundle() };
            return frag1;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_home, container, false);

            MenuPedido = view.FindViewById<RelativeLayout>(Resource.Id.MenuPedidos);
            MenuPreOrden = view.FindViewById<RelativeLayout>(Resource.Id.MenuPreOrden);
            imgPedidos = view.FindViewById<ImageView>(Resource.Id.imgPedidos);
            imgPreorden = view.FindViewById<ImageView>(Resource.Id.imgPreorden);

            txtCantidadPedidos= view.FindViewById<TextView>(Resource.Id.txtCantidadPedidos);
            txtCantidadPreOrden = view.FindViewById<TextView>(Resource.Id.txtCantidadPreOrden);

            MenuPedido.Click += MenuPedido_Click;
            MenuPreOrden.Click += MenuPreOrden_Click;

            if (VariablesGlobales.RolUsuario == 1)
            {
                MenuPedido.Visibility = ViewStates.Visible;
                imgPedidos.Visibility = ViewStates.Visible;
            }
            else if(VariablesGlobales.RolUsuario == 2)
            {
                MenuPreOrden.Visibility = ViewStates.Visible;
                imgPreorden.Visibility = ViewStates.Visible;
            }
            else
            {
                MenuPedido.Visibility = ViewStates.Visible;
                MenuPreOrden.Visibility = ViewStates.Visible;
                imgPedidos.Visibility = ViewStates.Visible;
                imgPreorden.Visibility = ViewStates.Visible;
            }

            CountTramites();

            return view;
        }

        private async void CountTramites()
        {
            var ListaPedidos = new List<PedidosViewModel>();
            var ListaPreorden = new List<OrdenViewModel>();

            GetPedOrdBindingModel GetPedOrdBindingModel = new GetPedOrdBindingModel();
            GetPedOrdBindingModel.CODUSR = VariablesGlobales.CodigoUsuario;
            GetPedOrdBindingModel.NROTKN = VariablesGlobales.Token;
            GetPedOrdBindingModel.SYSORI = Constantes.sysori;

            PedidoCore PedidoCore = new PedidoCore();
            ListaPedidos = await PedidoCore.ObtenerPedidos(GetPedOrdBindingModel);

            PreordenCore PreordenCore = new PreordenCore();
            ListaPreorden = await PreordenCore.ObtenerPreorden(GetPedOrdBindingModel);

            var pedidos = ListaPedidos.Count;
            var preordenes = ListaPreorden.Count;

            txtCantidadPedidos.Text = pedidos.ToString();
            txtCantidadPreOrden.Text = preordenes.ToString();
        }

        private void MenuPreOrden_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(PreOrdenListaActivity));
            StartActivity(intent);
        }

        private void MenuPedido_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(PedidosListaActivity));
            StartActivity(intent);
        }
    }
}