using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;

namespace ConsultasSP.Droid.Activities.Pedidos
{
    class PedidoItemsRecycler : RecyclerView.Adapter
    {
        public List<PedidosDetalleViewModel> mListaItemPedido { get; private set; }
        public event EventHandler<PedidoItemsRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PedidoItemsRecyclerClickEventArgs> ItemLongClick;
        //string[] items;

        public PedidoItemsRecycler(List<PedidosDetalleViewModel> ListaItemPedido)
        {
            mListaItemPedido = ListaItemPedido;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_pedido_items;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PedidoItemsRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaItemPedido[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PedidoItemsRecyclerViewHolder;
            holder.mProductoPedido.Text = item.DESPRO;
            holder.mDescripcionPedido.Text = item.DESPRO;
            holder.mCantidadPedido.Text = item.CANPRO.ToString();
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => mListaItemPedido.Count;

        void OnClick(PedidoItemsRecyclerClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PedidoItemsRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PedidoItemsRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public TextView mProductoPedido { get; set; }
        public TextView mDescripcionPedido { get; set; }
        public TextView mCantidadPedido { get; set; }

        public PedidoItemsRecyclerViewHolder(View itemView, Action<PedidoItemsRecyclerClickEventArgs> clickListener,
                            Action<PedidoItemsRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            mProductoPedido = itemView.FindViewById<TextView>(Resource.Id.ValorProductoPedido);
            mDescripcionPedido = itemView.FindViewById<TextView>(Resource.Id.ValorDescripcionPedido);
            mCantidadPedido = itemView.FindViewById<TextView>(Resource.Id.ValorCantidadPedido);
            itemView.Click += (sender, e) => clickListener(new PedidoItemsRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PedidoItemsRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PedidoItemsRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}