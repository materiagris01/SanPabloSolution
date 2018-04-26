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
using Android.Util;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using Newtonsoft.Json;
using ConsultasSP.Util.Commom;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    [Activity(Label = "PedidoActualizarActivity", Theme = "@style/AppTheme.PopMe", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PedidoActualizarActivity : Activity
    {
        internal static string intent_Actualizar;
        Button CancelarActualizar, ConfirmarActualizar;
        EditText ComentarioActualizar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_pedidosActualizar);

            CancelarActualizar = FindViewById<Button>(Resource.Id.CancelarActualizar);
            ConfirmarActualizar = FindViewById<Button>(Resource.Id.ConfirmarActualizar);
            ComentarioActualizar = FindViewById<EditText>(Resource.Id.ComentarioActualizar);

            DisplayMetrics dm = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(dm);

            int width = dm.WidthPixels;
            int height = dm.HeightPixels;

            Window.SetLayout((int)(width * .9), (int)(height * .4));
            SetFinishOnTouchOutside(false);

            WindowManagerLayoutParams param = Window.Attributes;
            param.Gravity = GravityFlags.Center;
            param.X = 0;
            param.Y = -20;
            Window.Attributes = param;
        }
    }
}