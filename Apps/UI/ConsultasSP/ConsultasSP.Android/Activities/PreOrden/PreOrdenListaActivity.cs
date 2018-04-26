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
using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.Core;
using ConsultasSP.Util.Commom;
using Android.Views.InputMethods;

namespace ConsultasSP.Droid.Activities.PreOrden
{
    [Activity(Label = "PreOrdenLista", Theme = "@style/AppTheme.NoActionBar", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PreOrdenListaActivity : AppCompatActivity
    {
        public List<OrdenViewModel> ListaPreorden { get; private set; }
        public RecyclerView RecyclerViewPreOrden;
        public LinearLayout SinPreorden, SinResultados, ConPreorden, bloqueBusqueda;
        private Context ContainerContext;

        //EDITTEXT PARA FILTRADO DE PEDIDOS
        private EditText patron;
        private bool estaVisible = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_preordenLista);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetTitle(Resource.String.preorden);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Mipmap.ic_back);
            ab.SetDisplayHomeAsUpEnabled(true);
            ContainerContext = this.ApplicationContext;

            SinPreorden = FindViewById<LinearLayout>(Resource.Id.SinPreorden);
            SinResultados = FindViewById<LinearLayout>(Resource.Id.SinResultados);
            ConPreorden = FindViewById<LinearLayout>(Resource.Id.ConPreorden);
            bloqueBusqueda = FindViewById<LinearLayout>(Resource.Id.bloqueBusqueda);

            RecyclerViewPreOrden = FindViewById<RecyclerView>(Resource.Id.RecyclerViewPreOrden);
            SetUpRecyclerViewListaOrden(RecyclerViewPreOrden);

            patron = FindViewById<EditText>(Resource.Id.txtFiltro);
            patron.TextChanged += Patron_TextChanged;
            patron.RequestFocus();
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
            List<OrdenViewModel> filtrados = new List<OrdenViewModel>();
            string searchText = patron.Text;

            foreach (var item in ListaPreorden)
            {
                if (Convert.ToString(item.NROTRA).StartsWith(searchText))
                {
                    filtrados.Add(item);
                }
            }

            if (filtrados.Count < 1)
            {
                ConPreorden.Visibility = ViewStates.Gone;
                SinResultados.Visibility = ViewStates.Visible;
                SinPreorden.Visibility = ViewStates.Gone;
            }
            else
            {
                ConPreorden.Visibility = ViewStates.Visible;
                SinResultados.Visibility = ViewStates.Gone;
                SinPreorden.Visibility = ViewStates.Gone;

                RecyclerViewPreOrden.SetAdapter(new PreOrdenListaRecycler(filtrados));
            }
        }
        private async void SetUpRecyclerViewListaOrden(RecyclerView recyclerViewPedidos)
        {
            var progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            ListaPreorden = new List<OrdenViewModel>();
            GetPedOrdBindingModel GetPedOrdBindingModel = new GetPedOrdBindingModel();
            GetPedOrdBindingModel.CODUSR = VariablesGlobales.CodigoUsuario;
            GetPedOrdBindingModel.NROTKN = VariablesGlobales.Token;
            GetPedOrdBindingModel.SYSORI = Constantes.sysori;

            PreordenCore PreordenCore = new PreordenCore();
            ListaPreorden = await PreordenCore.ObtenerPreorden(GetPedOrdBindingModel);

            recyclerViewPedidos.SetLayoutManager(new LinearLayoutManager(recyclerViewPedidos.Context));
            recyclerViewPedidos.SetAdapter(new PreOrdenListaRecycler(ListaPreorden));

            if (ListaPreorden.Count == 0)
            {
                SinPreorden.Visibility = ViewStates.Visible;
            }
            else
            {
                ConPreorden.Visibility = ViewStates.Visible;
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
            RecyclerViewPreOrden = FindViewById<RecyclerView>(Resource.Id.RecyclerViewPreOrden);
            SetUpRecyclerViewListaOrden(RecyclerViewPreOrden);
        }
    }
}