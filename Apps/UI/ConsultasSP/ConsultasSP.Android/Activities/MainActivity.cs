using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Java.Lang;

using Android.Util;
using Android.Content;
using ConsultasSP.Droid.Activities.Usuario;
using ConsultasSP.Droid.Fragments;
using ConsultasSP.Droid.Activities;
using ConsultasSP.Util.Commom;

namespace ConsultasSP.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity, IRunnable
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private bool doubleBackToExitPressedOnce;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Main;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //TextView Nombre = navigationView.FindViewById<TextView>(Resource.Id.NombreUsuario);
            //TextView Cargo = navigationView.FindViewById<TextView>(Resource.Id.CargoUsuario);

            //Nombre.Text = VariablesGlobales.NombreUsuario;
            //Cargo.Text = VariablesGlobales.CargoUsuario;


            navigationView.NavigationItemSelected += (sender, e) =>
            {

                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    //case Resource.Id.nav_home_1:
                    //    ListItemClicked(0);
                    //    break;
                    //case Resource.Id.nav_home_3:
                    //    StartActivity(typeof(ConfiguracionActivity));
                    //    break; 
                    case Resource.Id.nav_home_4:
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                        Android.Support.V7.App.AlertDialog alertDialog = null;
                        alert.SetTitle("San Pablo App");
                        alert.SetMessage("Cerrar sesión");
                        alert.SetPositiveButton("Salir", (senderAlert, args) =>
                        {
                            StartActivity(typeof(LoginActivity));
                            Finish();
                        });
                        alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                        {
                            alertDialog.Dismiss();
                        });

                        alertDialog = alert.Create();
                        alertDialog.Show();
                        break;

                }

                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked(0);
            }
        }
        int oldPosition = -1;
        private View UsuarioNavHeader;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            fragment = HomeFragment.NewInstance();
            //case 0:
            //    fragment = PedidosHomeFragment.NewInstance();
            //    SupportActionBar.SetTitle(Resource.String.menu_crear_pedidos);
            //    break;
            //case 1:
            //    StartActivity(typeof(HistorialActivity));
            //    break;
            //case 2:
            //    fragment = PedidosConfiguracionFragment.NewInstance();
            //    SupportActionBar.SetTitle(Resource.String.configuracion_mesa_titulo);                    
            //    break;

            SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            try
            {
                if (doubleBackToExitPressedOnce)
                {
                    this.MoveTaskToBack(true);
                    return;
                }

                this.doubleBackToExitPressedOnce = true;
                Toast.MakeText(this, GetString(Resource.String.app_name), ToastLength.Short).Show();
                new Handler().PostDelayed(this, 2000);
            }
            catch (System.Exception)
            {
                Finish();
            }
        }
        public void Run()
        {
            doubleBackToExitPressedOnce = false;
        }
    }
}

