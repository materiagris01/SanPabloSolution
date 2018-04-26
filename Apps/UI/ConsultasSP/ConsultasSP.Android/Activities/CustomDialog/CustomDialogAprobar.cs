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
using ConsultasSP.Core;
using ConsultasSP.CrossCutting.Dominio.BindingModels;
using ConsultasSP.Droid.Activities.Pedidos;
using ConsultasSP.Droid.Activities.PreOrden;
using ConsultasSP.Util.Commom;

namespace ConsultasSP.Droid.Activities.CustomDialog
{
    class CustomDialogAprobar : DialogFragment
    {
        int NROTRA;
        string ACTIVITY;
        Button confirmar, cancel;
        EditText comentario;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.activity_pedidosActualizar, container, false);
            //view.RequestWindowFeature((int)WindowFeatures.NoTitle);

            confirmar = view.FindViewById<Button>(Resource.Id.ConfirmarActualizar);
            cancel = view.FindViewById<Button>(Resource.Id.CancelarActualizar);
            comentario = view.FindViewById<EditText>(Resource.Id.ComentarioActualizar);

            NROTRA = Arguments.GetInt("NROTRA", 0);
            ACTIVITY = Arguments.GetString("ACTIVITY", string.Empty);
            //comentario.Text = NROTRA.ToString();
            cancel.Click += Cancel_Click;
            confirmar.Click += Confirmar_Click;


            return view;

        }

        private async void Confirmar_Click(object sender, EventArgs e)
        {
            var progress = new Android.App.ProgressDialog(this.Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetMessage(GetString(Resource.String.globales_loader));
            progress.SetMessage("Cargando");
            progress.SetCancelable(false);
            progress.Show();

            UsuarioTramiteBindingModel UsuarioTramiteBindingModel = new UsuarioTramiteBindingModel();
            UsuarioTramiteBindingModel.Usuario = new UsuarioBindingModel();
            UsuarioTramiteBindingModel.Usuario.CODUSR = VariablesGlobales.CodigoUsuario;
            UsuarioTramiteBindingModel.Usuario.NROTKN = VariablesGlobales.Token;
            UsuarioTramiteBindingModel.Tramite = new TramiteBindingModel();
            UsuarioTramiteBindingModel.Tramite.NROTRA = NROTRA;
            UsuarioTramiteBindingModel.Tramite.ESTADO = "AU";
            UsuarioTramiteBindingModel.Tramite.DESOBS = comentario.Text;



            if (ACTIVITY == "Pedidos")
            {
                PedidoCore PedidoCore = new PedidoCore();
                var result = await PedidoCore.ActualizarPedidos(UsuarioTramiteBindingModel);

                if (!String.IsNullOrEmpty(result.Tramite.NROTRA.ToString()))
                {
                    Intent intent = new Intent(this.Activity, typeof(PedidosListaActivity));
                    StartActivity(intent);
                    Dismiss();
                }
                else
                {
                    Toast.MakeText(this.Activity, "No se puede actualizar", ToastLength.Short).Show();
                    Dismiss();
                }


            }
            else
            {
                PreordenCore PreordenCore = new PreordenCore();
                var result = await PreordenCore.ActualizarPreordenes(UsuarioTramiteBindingModel);
                if (!String.IsNullOrEmpty(result.Tramite.NROTRA.ToString()))
                {
                    Intent intent = new Intent(this.Activity, typeof(PreOrdenListaActivity));
                    StartActivity(intent);
                    Dismiss();
                }
                else
                {
                    Toast.MakeText(this.Activity, "No se puede actualizar", ToastLength.Short).Show();
                    Dismiss();
                }

            }


            progress.Dismiss();

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }
}