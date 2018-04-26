using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;

namespace ConsultasSP.Droid.Activities.PreOrden
{
    class PreOrdenItemsRecycler : RecyclerView.Adapter
    {
        public List<OrdenDetalleViewModel> mListaItemPedido { get; private set; }
        public event EventHandler<PreOrdenItemsRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PreOrdenItemsRecyclerClickEventArgs> ItemLongClick;

        public PreOrdenItemsRecycler(List<OrdenDetalleViewModel> ListaItemPreorden)
        {
            mListaItemPedido = ListaItemPreorden;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_preorden_items;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PreOrdenItemsRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaItemPedido[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PreOrdenItemsRecyclerViewHolder;
            holder.mValorProductoPreorden.Text = item.CODPRO.ToString();
            holder.mValorDescripcionPreorden.Text = item.DESPRO;
            holder.mValorUnidadPreorden.Text = item.CODUNI;
            holder.mValorCantidadPreorden.Text = item.CANPRO.ToString();
            holder.mValorUnitarioPreorden.Text = item.VALUNI.ToString();
            holder.mValorTotalPreorden.Text = item.VALTOT.ToString();
        }

        public override int ItemCount => mListaItemPedido.Count;

        void OnClick(PreOrdenItemsRecyclerClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PreOrdenItemsRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PreOrdenItemsRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public TextView mValorProductoPreorden { get; set; }
        public TextView mValorDescripcionPreorden { get; set; }
        public TextView mValorUnidadPreorden { get; set; }
        public TextView mValorCantidadPreorden { get; set; }
        public TextView mValorUnitarioPreorden { get; set; }
        public TextView mValorTotalPreorden { get; set; }

        public PreOrdenItemsRecyclerViewHolder(View itemView, Action<PreOrdenItemsRecyclerClickEventArgs> clickListener,
                            Action<PreOrdenItemsRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            mValorProductoPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorProductoPreorden);
            mValorDescripcionPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorDescripcionPreorden);
            mValorUnidadPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorUnidadPreorden);
            mValorCantidadPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorCantidadPreorden);
            mValorUnitarioPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorUnitarioPreorden);
            mValorTotalPreorden = itemView.FindViewById<TextView>(Resource.Id.ValorTotalPreorden);
            itemView.Click += (sender, e) => clickListener(new PreOrdenItemsRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PreOrdenItemsRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PreOrdenItemsRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}