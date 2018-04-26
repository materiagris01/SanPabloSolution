using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using System.IO;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    class PedidoAdjuntosRecycler : RecyclerView.Adapter
    {
        public List<PedidosAdjuntosViewModel> mListaAdjuntoPedido { get; private set; }
        public event EventHandler<PedidoAdjuntosRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PedidoAdjuntosRecyclerClickEventArgs> ItemLongClick;
        //string[] items;

        public PedidoAdjuntosRecycler(List<PedidosAdjuntosViewModel> ListaAdjuntoPedido)
        {
            mListaAdjuntoPedido = ListaAdjuntoPedido;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_pedido_adjuntos;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PedidoAdjuntosRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaAdjuntoPedido[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PedidoAdjuntosRecyclerViewHolder;
            holder.mNombrePdfPedido.Text = String.Format("{0}.{1}",item.DESADJ, item.FORMAT);
            holder.mFechaPdfPedido.Text = DateTime.Now.ToString();
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => mListaAdjuntoPedido.Count;

        void OnClick(PedidoAdjuntosRecyclerClickEventArgs args) => PedidoAdjunto(args);

        private void PedidoAdjunto(PedidoAdjuntosRecyclerClickEventArgs args)
        {
            var Name = "Download";
            byte[] byteArray = Convert.FromBase64String(mListaAdjuntoPedido[args.Position].CONTEN);
            var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            directory = Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
            string filePath = Path.Combine(directory.ToString(), Name);
            File.WriteAllBytes(filePath, byteArray);

            Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(pdfPath, "application/pdf");
            intent.SetFlags(ActivityFlags.NewTask);

            try
            {
                args.View.Context.StartActivity(intent);
            }
            catch (Exception)
            {
                Toast.MakeText(args.View.Context, "No Application Available to View PDF", ToastLength.Short).Show();
            }


        }

        void OnLongClick(PedidoAdjuntosRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PedidoAdjuntosRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public TextView mNombrePdfPedido { get; set; }
        public TextView mFechaPdfPedido { get; set; }

        public PedidoAdjuntosRecyclerViewHolder(View itemView, Action<PedidoAdjuntosRecyclerClickEventArgs> clickListener,
                            Action<PedidoAdjuntosRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            mNombrePdfPedido = itemView.FindViewById<TextView>(Resource.Id.NombrePdfPedido);
            mFechaPdfPedido = itemView.FindViewById<TextView>(Resource.Id.FechaPdfPedido);
            itemView.Click += (sender, e) => clickListener(new PedidoAdjuntosRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PedidoAdjuntosRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PedidoAdjuntosRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}