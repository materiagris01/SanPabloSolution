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

namespace ConsultasSP.Droid.Activities.PreOrden
{
    [Activity(Label = "PreOrdenDetalle", Theme = "@style/AppTheme.NoActionBar", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PreOrdenDetalleActivity : AppCompatActivity
    {
        public List<OrdenDetalleViewModel> ListaItemPreorden { get; private set; }
        public List<OrdenAdjuntosViewModel> ListaAdjuntoPreorden { get; private set; }
        public RecyclerView RecyclerViewItemPreorden, RecyclerViewAdjuntoPreorden;
        internal static string intent_PreordenDetalle;
        private Context ContainerContext;
        TextView ValorTotalPreorden, ValorDescuentoPreorden, ValorFechaPreorden, ValorProveedorPreorden, ValorCondicionPreorden, ValorSupervisorPreorden, ValorCompradorPreorden;
        Button DetallesPreorden, AdjuntosPreorden;
        ImageButton RechazarPreorden, AprobarPreorden;
        RelativeLayout Detalles, Adjuntos;
        LinearLayout SinPreordenDetalles, ConPreordenDetalles, SinPreordenAdjuntos, ConPreordenAdjuntos;
        public OrdenViewModel Preorden { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_preordenDetalle);

            Preorden = JsonConvert.DeserializeObject<OrdenViewModel>(Intent.GetStringExtra(intent_PreordenDetalle));
            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.Title = Preorden.NROTRA.ToString();
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Mipmap.ic_back);
            ab.SetDisplayHomeAsUpEnabled(true);
            ContainerContext = this.ApplicationContext;

            ValorTotalPreorden = FindViewById<TextView>(Resource.Id.ValorTotalPreorden);
            ValorDescuentoPreorden = FindViewById<TextView>(Resource.Id.ValorDescuentoPreorden);
            ValorFechaPreorden = FindViewById<TextView>(Resource.Id.ValorFechaPreorden);
            ValorProveedorPreorden = FindViewById<TextView>(Resource.Id.ValorProveedorPreorden);
            ValorCondicionPreorden = FindViewById<TextView>(Resource.Id.ValorCondicionPreorden);
            ValorSupervisorPreorden = FindViewById<TextView>(Resource.Id.ValorSupervisorPreorden);
            ValorCompradorPreorden = FindViewById<TextView>(Resource.Id.ValorCompradorPreorden);

            DetallesPreorden = FindViewById<Button>(Resource.Id.DetallesPreorden);
            AdjuntosPreorden = FindViewById<Button>(Resource.Id.AdjuntosPreorden);

            Detalles = FindViewById<RelativeLayout>(Resource.Id.Detalles);
            Adjuntos = FindViewById<RelativeLayout>(Resource.Id.Adjuntos);
            SinPreordenDetalles = FindViewById<LinearLayout>(Resource.Id.SinPreordenDetalles);
            ConPreordenDetalles = FindViewById<LinearLayout>(Resource.Id.ConPreordenDetalles);
            SinPreordenAdjuntos = FindViewById<LinearLayout>(Resource.Id.SinPreordenAdjuntos);
            ConPreordenAdjuntos = FindViewById<LinearLayout>(Resource.Id.ConPreordenAdjuntos);

            RechazarPreorden = FindViewById<ImageButton>(Resource.Id.RechazarPreorden);
            AprobarPreorden = FindViewById<ImageButton>(Resource.Id.AprobarPreorden);

            ValorTotalPreorden.Text = String.Format("S/. {0:0.00}", Preorden.VALTOT);
            ValorDescuentoPreorden.Text = String.Format("S/. {0:0.00}", "0");
            ValorFechaPreorden.Text = Preorden.FECDOC.ToString("dd/MM/yyyy hh:mm tt");
            ValorProveedorPreorden.Text = Preorden.DESPRV;
            ValorCondicionPreorden.Text = Preorden.DESCON;
            ValorSupervisorPreorden.Text = Preorden.DESSUP;
            ValorCompradorPreorden.Text = Preorden.DESCOM;

            DetallesPreorden.Click += DetallesPreorden_Click;
            AdjuntosPreorden.Click += AdjuntosPreorden_Click;
            RechazarPreorden.Click += RechazarPreorden_Click;
            AprobarPreorden.Click += AprobarPreorden_Click;

            RecyclerViewItemPreorden = FindViewById<RecyclerView>(Resource.Id.RecyclerViewItemPreorden);
            SetUpRecyclerViewItemPreorden(RecyclerViewItemPreorden);

            RecyclerViewAdjuntoPreorden = FindViewById<RecyclerView>(Resource.Id.RecyclerViewAdjuntoPreorden);
            SetUpRecyclerViewAdjuntoPreorden(RecyclerViewAdjuntoPreorden);
        }

        private void AprobarPreorden_Click(object sender, EventArgs e)
        {
            Bundle utilBundle = new Bundle();
            utilBundle.PutInt("NROTRA", Preorden.NROTRA);
            utilBundle.PutString("ACTIVITY", "Preorden");
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            CustomDialogAprobar customDialog = new CustomDialogAprobar { Arguments = utilBundle };
            customDialog.Show(transcation, "Dialog Fragment");
        }

        private void RechazarPreorden_Click(object sender, EventArgs e)
        {
            Bundle utilBundle = new Bundle();
            utilBundle.PutInt("NROTRA", Preorden.NROTRA);
            utilBundle.PutString("ACTIVITY", "Preorden");
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            CustomDialogRechazar customDialog = new CustomDialogRechazar { Arguments = utilBundle };
            customDialog.Show(transcation, "Dialog Fragment");
        }

        private async void SetUpRecyclerViewAdjuntoPreorden(RecyclerView recyclerViewAdjuntoPreorden)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaAdjuntoPreorden = new List<OrdenAdjuntosViewModel>();
            UsuarioTramiteBindingModel UsuarioTramiteBindingModel = new UsuarioTramiteBindingModel();
            UsuarioTramiteBindingModel.Usuario = new UsuarioBindingModel();
            UsuarioTramiteBindingModel.Usuario.CODUSR = VariablesGlobales.NombreUsuario;
            UsuarioTramiteBindingModel.Usuario.NROTKN = VariablesGlobales.Token;
            UsuarioTramiteBindingModel.Tramite = new TramiteBindingModel();
            UsuarioTramiteBindingModel.Tramite.NROTRA = Preorden.NROTRA;
            //UsuarioTramiteBindingModel.SYSORI = Constantes.sysori;

            PreordenCore PreordenCore = new PreordenCore();
            ListaAdjuntoPreorden = await PreordenCore.ObtenerAdjuntosPreorden(UsuarioTramiteBindingModel);
            recyclerViewAdjuntoPreorden.SetLayoutManager(new LinearLayoutManager(recyclerViewAdjuntoPreorden.Context));
            recyclerViewAdjuntoPreorden.SetAdapter(new PreOrdenAdjuntosRecycler(ListaAdjuntoPreorden));

            if (ListaAdjuntoPreorden.Count == 0)
            {
                SinPreordenAdjuntos.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPreordenAdjuntos.Visibility = ViewStates.Visible;
            }

            progress.Dismiss();
        }

        private async void SetUpRecyclerViewItemPreorden(RecyclerView recyclerViewItemPreorden)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaItemPreorden = new List<OrdenDetalleViewModel>();
            UsuarioTramiteBindingModel UsuarioTramiteBindingModel = new UsuarioTramiteBindingModel();
            UsuarioTramiteBindingModel.Usuario = new UsuarioBindingModel();
            UsuarioTramiteBindingModel.Usuario.CODUSR = VariablesGlobales.CodigoUsuario;
            UsuarioTramiteBindingModel.Usuario.NROTKN = VariablesGlobales.Token;
            UsuarioTramiteBindingModel.Tramite = new TramiteBindingModel();
            UsuarioTramiteBindingModel.Tramite.NROTRA = Preorden.NROTRA;
            //UsuarioTramiteBindingModel.SYSORI = Constantes.sysori;

            PreordenCore PreordenCore = new PreordenCore();
            ListaItemPreorden = await PreordenCore.ObtenerDetallePreorden(UsuarioTramiteBindingModel);
            recyclerViewItemPreorden.SetLayoutManager(new LinearLayoutManager(recyclerViewItemPreorden.Context));
            recyclerViewItemPreorden.SetAdapter(new PreOrdenItemsRecycler(ListaItemPreorden));

            if (ListaItemPreorden.Count == 0)
            {
                SinPreordenDetalles.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPreordenDetalles.Visibility = ViewStates.Visible;
            }

            progress.Dismiss();
        }

        private void AdjuntosPreorden_Click(object sender, EventArgs e)
        {
            Detalles.Visibility = ViewStates.Gone;
            Adjuntos.Visibility = ViewStates.Visible;

            AdjuntosPreorden.SetBackgroundResource(Resource.Drawable.border_radius_white);
            var draw = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info_azul);
            AdjuntosPreorden.SetCompoundDrawablesWithIntrinsicBounds(draw, null, null, null);
            AdjuntosPreorden.SetTextColor(Android.Graphics.Color.Blue);

            DetallesPreorden.SetBackgroundResource(Resource.Drawable.border_radius_blue);
            var draw2 = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info);
            DetallesPreorden.SetCompoundDrawablesWithIntrinsicBounds(draw2, null, null, null);
            DetallesPreorden.SetTextColor(Android.Graphics.Color.White);
        }

        private void DetallesPreorden_Click(object sender, EventArgs e)
        {
            Detalles.Visibility = ViewStates.Visible;
            Adjuntos.Visibility = ViewStates.Gone;
            AdjuntosPreorden.SetBackgroundResource(Resource.Drawable.border_radius_blue);
            var draw = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info);
            AdjuntosPreorden.SetCompoundDrawablesWithIntrinsicBounds(draw, null, null, null);
            AdjuntosPreorden.SetTextColor(Android.Graphics.Color.White);

            DetallesPreorden.SetBackgroundResource(Resource.Drawable.border_radius_white);
            var draw2 = ContextCompat.GetDrawable(this, Resource.Mipmap.ic_info_azul);
            DetallesPreorden.SetCompoundDrawablesWithIntrinsicBounds(draw2, null, null, null);
            DetallesPreorden.SetTextColor(Android.Graphics.Color.Blue);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Intent intent = new Intent(this, typeof(PreOrdenListaActivity));
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
            Intent intent = new Intent(this, typeof(PreOrdenListaActivity));
            StartActivity(intent);
            Finish();
        }
    }
}