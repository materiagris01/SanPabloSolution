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
using ConsultasSP.Core;
using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.Util.Commom;
using Android.Views.InputMethods;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    [Activity(Label = "PedidosLista", Theme = "@style/AppTheme.NoActionBar", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PedidosListaActivity : AppCompatActivity
    {
        public List<PedidosViewModel> ListaPedidos { get; private set; }
        public RecyclerView RecyclerViewPedidos;
        public LinearLayout SinPedidos, SinResultados, ConPedidos, bloqueBusqueda;
        private Context ContainerContext;
        //private SwipeRefreshLayout mSwipeRefreshLayout;
        //ProgressDialog progress;

        //EDITTEXT PARA FILTRADO DE PEDIDOS
        private EditText patron;
        private bool estaVisible = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_pedidosLista);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetTitle(Resource.String.pedidos);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Mipmap.ic_back);
            ab.SetDisplayHomeAsUpEnabled(true);
            ContainerContext = this.ApplicationContext;

            SinPedidos = FindViewById<LinearLayout>(Resource.Id.SinPedidos);
            SinResultados = FindViewById<LinearLayout>(Resource.Id.SinResultados);
            ConPedidos = FindViewById<LinearLayout>(Resource.Id.ConPedidos);
            bloqueBusqueda = FindViewById<LinearLayout>(Resource.Id.bloqueBusqueda);

            RecyclerViewPedidos = FindViewById<RecyclerView>(Resource.Id.RecyclerViewPedidos);
            SetUpRecyclerViewListaPedidos(RecyclerViewPedidos);

            patron = FindViewById<EditText>(Resource.Id.txtFiltro);
            patron.TextChanged += Patron_TextChanged;
            patron.EditorAction += Patron_EditorAction;
        }

        private void Patron_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Search)
            {
                OcultaTeclado();
            }
        }

        private void OcultaTeclado()
        {
            var view = CurrentFocus;
            if (view != null)
            {
                var imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }

        private void Patron_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<PedidosViewModel> filtrados = new List<PedidosViewModel>();
            string searchText = patron.Text;

            foreach (var item in ListaPedidos)
            {
                if (Convert.ToString(item.NROTRA).StartsWith(searchText))
                {
                    filtrados.Add(item);
                }
            }

            if (filtrados.Count < 1)
            {
                ConPedidos.Visibility = ViewStates.Gone;
                SinResultados.Visibility = ViewStates.Visible;
                SinPedidos.Visibility = ViewStates.Gone;
            }
            else
            {
                ConPedidos.Visibility = ViewStates.Visible;
                SinResultados.Visibility = ViewStates.Gone;
                SinPedidos.Visibility = ViewStates.Gone;

                RecyclerViewPedidos.SetAdapter(new PedidoListaRecycler(filtrados));
            }
        }
        private async void SetUpRecyclerViewListaPedidos(RecyclerView recyclerViewPedidos)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaPedidos = new List<PedidosViewModel>();
            GetPedOrdBindingModel GetPedOrdBindingModel = new GetPedOrdBindingModel();
            GetPedOrdBindingModel.CODUSR = VariablesGlobales.CodigoUsuario;
            GetPedOrdBindingModel.NROTKN = VariablesGlobales.Token;
            GetPedOrdBindingModel.SYSORI = Constantes.sysori;

            PedidoCore PedidoCore = new PedidoCore();
            ListaPedidos = await PedidoCore.ObtenerPedidos(GetPedOrdBindingModel);
            recyclerViewPedidos.SetLayoutManager(new LinearLayoutManager(recyclerViewPedidos.Context));
            recyclerViewPedidos.SetAdapter(new PedidoListaRecycler(ListaPedidos));

            if (ListaPedidos.Count == 0)
            {
                SinPedidos.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPedidos.Visibility = ViewStates.Visible;
            }

            progress.Dismiss();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.nav_search, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    StartActivity(typeof(MainActivity));
                    Finish();
                    break;
                case Resource.Id.action_search:
                    toogleBusqueda(this.estaVisible);
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return base.OnOptionsItemSelected(item);
        }

        public void toogleBusqueda(bool x)
        {
            if (x == false)
            {
                MostrarTeclado();
                bloqueBusqueda.Visibility = ViewStates.Visible;
                this.estaVisible = true;
            }
            else
            {
                OcultaTeclado();
                bloqueBusqueda.Visibility = ViewStates.Gone;
                this.estaVisible = false;
            }
        }

        private void MostrarTeclado()
        {
            patron.Text = "";
            patron.RequestFocus();
            var imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.ShowSoftInput(patron, ShowFlags.Implicit);
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        protected override void OnResume()
        {
            base.OnResume();
            RecyclerViewPedidos = FindViewById<RecyclerView>(Resource.Id.RecyclerViewPedidos);
            SetUpRecyclerViewListaPedidos(RecyclerViewPedidos);
        }
    }
}