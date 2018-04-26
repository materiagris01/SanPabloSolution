using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using Newtonsoft.Json;
using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.Core;
using ConsultasSP.Util.Commom;
using ConsultasSP.Droid.Activities.CustomDialog;
using Android.Support.V4.Content;
using System.Drawing;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    [Activity(Label = "PedidosDetalle", Theme = "@style/AppTheme.NoActionBar", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PedidosDetalleActivity : AppCompatActivity
    {
        public List<PedidosDetalleViewModel> ListaItemPedido { get; private set; }
        public List<PedidosAdjuntosViewModel> ListaAdjuntoPedido { get; private set; }
        public RecyclerView RecyclerViewItemPedido, RecyclerViewAdjuntoPedido;
        private Context ContainerContext;
        internal static string intent_PedidoDetalle;
        TextView ValorFechaPedido, ValorCompaniaPedido, ValorSucursalPedido, ValorSeccionPedido, ValorSolicitantePedido, ValorObservacionPedido;
        Button DetallesPedido, AdjuntosPedido;
        ImageButton RechazarPedido, AprobarPedido;
        RelativeLayout Detalles, Adjuntos;
        LinearLayout SinPedidoDetalles, ConPedidoDetalles, SinPedidoAdjuntos, ConPedidoAdjuntos;
        public LinearLayout SinPedidos, ConPedidos;
        public PedidosViewModel Pedidos { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_pedidosDetalle);

            Pedidos = JsonConvert.DeserializeObject<PedidosViewModel>(Intent.GetStringExtra(intent_PedidoDetalle));

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.Title = Pedidos.NROTRA.ToString();
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Mipmap.ic_back);
            ab.SetDisplayHomeAsUpEnabled(true);
            ContainerContext = this.ApplicationContext;

            ValorFechaPedido = FindViewById<TextView>(Resource.Id.ValorFechaPedido);
            ValorCompaniaPedido = FindViewById<TextView>(Resource.Id.ValorCompaniaPedido);
            ValorSucursalPedido = FindViewById<TextView>(Resource.Id.ValorSucursalPedido);
            ValorSeccionPedido = FindViewById<TextView>(Resource.Id.ValorSeccionPedido);
            ValorSolicitantePedido = FindViewById<TextView>(Resource.Id.ValorSolicitantePedido);
            ValorObservacionPedido = FindViewById<TextView>(Resource.Id.ValorObservacionPedido);

            DetallesPedido = FindViewById<Button>(Resource.Id.DetallesPedido);
            AdjuntosPedido = FindViewById<Button>(Resource.Id.AdjuntosPedido);

            Detalles = FindViewById<RelativeLayout>(Resource.Id.Detalles);
            Adjuntos = FindViewById<RelativeLayout>(Resource.Id.Adjuntos);
            SinPedidoDetalles = FindViewById<LinearLayout>(Resource.Id.SinPedidoDetalles);
            ConPedidoDetalles = FindViewById<LinearLayout>(Resource.Id.ConPedidoDetalles);
            SinPedidoAdjuntos = FindViewById<LinearLayout>(Resource.Id.SinPedidoAdjuntos);
            ConPedidoAdjuntos = FindViewById<LinearLayout>(Resource.Id.ConPedidoAdjuntos);

            RechazarPedido = FindViewById<ImageButton>(Resource.Id.RechazarPedido);
            AprobarPedido = FindViewById<ImageButton>(Resource.Id.AprobarPedido);

            ValorFechaPedido.Text = Pedidos.FECDOC.ToString();
            ValorCompaniaPedido.Text = Pedidos.DESEMP;
            ValorSucursalPedido.Text = Pedidos.DESSUC;
            ValorSeccionPedido.Text = Pedidos.DESSEC;
            ValorSolicitantePedido.Text = Pedidos.DESSOL;
            ValorObservacionPedido.Text = Pedidos.DESOBS;

            DetallesPedido.Click += DetallesPedido_Click;
            AdjuntosPedido.Click += AdjuntosPedido_Click;
            RechazarPedido.Click += RechazarPedido_Click;
            AprobarPedido.Click += AprobarPedido_Click;

            RecyclerViewItemPedido = FindViewById<RecyclerView>(Resource.Id.RecyclerViewItemPedido);
            SetUpRecyclerViewItemPedido(RecyclerViewItemPedido);

            RecyclerViewAdjuntoPedido = FindViewById<RecyclerView>(Resource.Id.RecyclerViewAdjuntoPedido);
            SetUpRecyclerViewAdjuntoPedido(RecyclerViewAdjuntoPedido);
        }

        private void AprobarPedido_Click(object sender, EventArgs e)
        {
            Bundle utilBundle = new Bundle();
            utilBundle.PutInt("NROTRA", Pedidos.NROTRA);
            utilBundle.PutString("ACTIVITY", "Pedidos");
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            CustomDialogAprobar customDialog = new CustomDialogAprobar { Arguments = utilBundle };
            customDialog.Show(transcation, "Dialog Fragment");
        }

        private void RechazarPedido_Click(object sender, EventArgs e)
        {
            Bundle utilBundle = new Bundle();
            utilBundle.PutInt("NROTRA", Pedidos.NROTRA);
            utilBundle.PutString("ACTIVITY", "Pedidos");
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            CustomDialogRechazar customDialog = new CustomDialogRechazar { Arguments = utilBundle };
            customDialog.Show(transcation, "Dialog Fragment");
        }

        private void AdjuntosPedido_Click(object sender, EventArgs e)
        {
            Detalles.Visibility = ViewStates.Gone;
            Adjuntos.Visibility = ViewStates.Visible;
            AdjuntosPedido.SetBackgroundResource(Resource.Drawable.border_radius_white);
            var draw = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info_azul);
            AdjuntosPedido.SetCompoundDrawablesWithIntrinsicBounds(draw, null, null, null);
            AdjuntosPedido.SetTextColor(Android.Graphics.Color.Blue);

            DetallesPedido.SetBackgroundResource(Resource.Drawable.border_radius_blue);
            var draw2 = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info);
            DetallesPedido.SetCompoundDrawablesWithIntrinsicBounds(draw2, null, null, null);
            DetallesPedido.SetTextColor(Android.Graphics.Color.White);
        }

        private void DetallesPedido_Click(object sender, EventArgs e)
        {
            Detalles.Visibility = ViewStates.Visible;
            Adjuntos.Visibility = ViewStates.Gone;
            AdjuntosPedido.SetBackgroundResource(Resource.Drawable.border_radius_blue);
            var draw = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info);
            AdjuntosPedido.SetCompoundDrawablesWithIntrinsicBounds(draw, null, null, null);
            AdjuntosPedido.SetTextColor(Android.Graphics.Color.White);

            DetallesPedido.SetBackgroundResource(Resource.Drawable.border_radius_white);
            var draw2 = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info_azul);
            DetallesPedido.SetCompoundDrawablesWithIntrinsicBounds(draw2, null, null, null);
            DetallesPedido.SetTextColor(Android.Graphics.Color.Blue);

        }

        private async void SetUpRecyclerViewItemPedido(RecyclerView recyclerViewItemPedido)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaItemPedido = new List<PedidosDetalleViewModel>();
            UsuarioTramiteBindingModel UsuarioTramiteBindingModel = new UsuarioTramiteBindingModel();
            UsuarioTramiteBindingModel.Usuario = new UsuarioBindingModel();
            UsuarioTramiteBindingModel.Usuario.CODUSR = VariablesGlobales.CodigoUsuario;
            UsuarioTramiteBindingModel.Usuario.NROTKN = VariablesGlobales.Token;
            UsuarioTramiteBindingModel.Tramite = new TramiteBindingModel();
            UsuarioTramiteBindingModel.Tramite.NROTRA = Pedidos.NROTRA;
            //UsuarioTramiteBindingModel.SYSORI = Constantes.sysori;

            PedidoCore PedidoCore = new PedidoCore();
            ListaItemPedido = await PedidoCore.ObtenerDetallePedidos(UsuarioTramiteBindingModel);
            recyclerViewItemPedido.SetLayoutManager(new LinearLayoutManager(recyclerViewItemPedido.Context));
            recyclerViewItemPedido.SetAdapter(new PedidoItemsRecycler(ListaItemPedido));

            if (ListaItemPedido.Count == 0)
            {
                SinPedidoDetalles.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPedidoDetalles.Visibility = ViewStates.Visible;
            }

            progress.Dismiss();
        }

        private async void SetUpRecyclerViewAdjuntoPedido(RecyclerView recyclerViewAdjuntoPedido)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaAdjuntoPedido = new List<PedidosAdjuntosViewModel>();
            UsuarioTramiteBindingModel UsuarioTramiteBindingModel = new UsuarioTramiteBindingModel();
            UsuarioTramiteBindingModel.Usuario = new UsuarioBindingModel();
            UsuarioTramiteBindingModel.Usuario.CODUSR = VariablesGlobales.CodigoUsuario;
            UsuarioTramiteBindingModel.Usuario.NROTKN = VariablesGlobales.Token;
            UsuarioTramiteBindingModel.Tramite = new TramiteBindingModel();
            UsuarioTramiteBindingModel.Tramite.NROTRA = Pedidos.NROTRA;
            //UsuarioTramiteBindingModel.SYSORI = Constantes.sysori;

            PedidoCore PedidoCore = new PedidoCore();
            ListaAdjuntoPedido = await PedidoCore.ObtenerAdjuntosPedidos(UsuarioTramiteBindingModel);
            recyclerViewAdjuntoPedido.SetLayoutManager(new LinearLayoutManager(recyclerViewAdjuntoPedido.Context));
            recyclerViewAdjuntoPedido.SetAdapter(new PedidoAdjuntosRecycler(ListaAdjuntoPedido));

            if (ListaAdjuntoPedido.Count == 0)
            {
                SinPedidoAdjuntos.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPedidoAdjuntos.Visibility = ViewStates.Visible;
            }

            progress.Dismiss();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Intent intent = new Intent(this, typeof(PedidosListaActivity));
                    StartActivity(intent);
                    Finish();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(PedidosListaActivity));
            StartActivity(intent);
            Finish();
        }

    }
}