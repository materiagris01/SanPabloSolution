using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    class PedidoListaRecycler : RecyclerView.Adapter
    {
        public List<PedidosViewModel> mListaPedidos { get; private set; }
        public event EventHandler<PedidoListaRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PedidoListaRecyclerClickEventArgs> ItemLongClick;
        //string[] items;

        public PedidoListaRecycler(List<PedidosViewModel> ListaPedidos)
        {
            mListaPedidos = ListaPedidos;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_pedido;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PedidoListaRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaPedidos[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PedidoListaRecyclerViewHolder;
            holder.mCodigoPedido.Text = item.NROTRA.ToString();
            holder.mItemPedido.Text = item.CANITE.ToString();
            holder.mFechaPedido.Text = item.FECDOC.ToString("dd/MM/yyyy hh:mm tt");
            holder.mProveedorPedido.Text = item.DESEMP;
            holder.mUsuarioPedido.Text = item.DESSUC;
            holder.mSupervisorPedido.Text = String.Format("{0} / {1}",item.DESSOL, "");
        }

        public override int ItemCount => mListaPedidos.Count;

        void OnClick(PedidoListaRecyclerClickEventArgs args) => PedidoDetalle(args);

        private void PedidoDetalle(PedidoListaRecyclerClickEventArgs args)
        {
            //Context context = rView.Context;
            Intent intent = new Intent(args.View.Context, typeof(PedidosDetalleActivity));
            intent.PutExtra(PedidosDetalleActivity.intent_PedidoDetalle, JsonConvert.SerializeObject(mListaPedidos[args.Position]));
            args.View.Context.StartActivity(intent);
        }

        void OnLongClick(PedidoListaRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PedidoListaRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public View mIndicadorEstado { get; set; }
        public TextView mCodigoPedido { get; set; }
        public TextView mItemPedido { get; set; }
        public TextView mFechaPedido { get; set; }
        public TextView mProveedorPedido { get; set; }
        public TextView mUsuarioPedido { get; set; }
        public TextView mSupervisorPedido { get; set; }

        public PedidoListaRecyclerViewHolder(View itemView, Action<PedidoListaRecyclerClickEventArgs> clickListener,
                            Action<PedidoListaRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            mIndicadorEstado = itemView.FindViewById<View>(Resource.Id.IndicadorEstado);
            mCodigoPedido = itemView.FindViewById<TextView>(Resource.Id.CodigoPedido);
            mItemPedido = itemView.FindViewById<TextView>(Resource.Id.ItemPedido);
            mFechaPedido = itemView.FindViewById<TextView>(Resource.Id.FechaPedido);
            mProveedorPedido = itemView.FindViewById<TextView>(Resource.Id.ProveedorPedido);
            mUsuarioPedido = itemView.FindViewById<TextView>(Resource.Id.UsuarioPedido);
            mSupervisorPedido = itemView.FindViewById<TextView>(Resource.Id.SupervisorPedido);
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new PedidoListaRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PedidoListaRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PedidoListaRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }

    
}