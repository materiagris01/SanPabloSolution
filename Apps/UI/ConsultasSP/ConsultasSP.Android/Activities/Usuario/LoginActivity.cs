using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Content.PM;
using System.Timers;
using Newtonsoft.Json;
using Android.Views.InputMethods;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using ConsultasSP.Core;
using ConsultasSP.Util.Commom;

namespace ConsultasSP.Droid.Activities.Usuario
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", WindowSoftInputMode = SoftInput.StateHidden, LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : AppCompatActivity
    {
        EditText usuarioLogin, passwordLogin;
        string usuario, clave;
        Button ingresar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_login);

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            usuarioLogin = FindViewById<EditText>(Resource.Id.usuarioLogin);
            passwordLogin = FindViewById<EditText>(Resource.Id.passwordLogin);
            ingresar = FindViewById<Button>(Resource.Id.IniciarSesionButton);

            ingresar.Click += Ingresar_Click;
            passwordLogin.EditorAction += PasswordLogin_EditorAction;
        }

        private void PasswordLogin_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Send)
            {
                var view = CurrentFocus;
                if (view != null)
                {
                    var imm = (InputMethodManager)GetSystemService(InputMethodService);
                    imm.HideSoftInputFromWindow(view.WindowToken, 0);
                }
                ingresar.PerformClick();
            }
        }

        private async void Ingresar_Click(object sender, EventArgs e)
        {
            usuario = usuarioLogin.Text;
            clave = passwordLogin.Text;

            if(!String.IsNullOrEmpty(usuario) || !String.IsNullOrEmpty(clave))
            {
                var progress = new Android.App.ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                //progress.SetMessage(GetString(Resource.String.globales_loader));
                progress.SetMessage("Cargando");
                progress.SetCancelable(false);
                progress.Show();

                UsuarioViewModel UsuarioViewModel = new UsuarioViewModel();
                UsuarioViewModel.CODUSR = usuario.ToUpper();
                UsuarioViewModel.CLAUSR = clave.ToUpper();

                UsuarioCore UsuarioCore = new UsuarioCore();
                DatosUsuarioViewModel DatosUsuarioViewModel = new DatosUsuarioViewModel();
                DatosUsuarioViewModel = await UsuarioCore.LoginUsuario(UsuarioViewModel);

                if (!String.IsNullOrEmpty(DatosUsuarioViewModel.CODUSR))
                {
                    if(DatosUsuarioViewModel.ROL != 0)
                    {
                        VariablesGlobales.CodigoUsuario = DatosUsuarioViewModel.CODUSR;
                        VariablesGlobales.Token = DatosUsuarioViewModel.NROTOK;
                        VariablesGlobales.NombreUsuario = DatosUsuarioViewModel.USUARIO;
                        VariablesGlobales.CargoUsuario = DatosUsuarioViewModel.CARGO;
                        VariablesGlobales.RolUsuario = DatosUsuarioViewModel.ROL;

                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "El usuario no cuenta con permisos", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Los datos ingresado no son los correctos", ToastLength.Short).Show();
                }

                progress.Dismiss();
            }
            else
            {
                Toast.MakeText(this, "Por favor, llenar los campos solicitados", ToastLength.Short).Show();
            }

            //Finish();
        }
    }
}