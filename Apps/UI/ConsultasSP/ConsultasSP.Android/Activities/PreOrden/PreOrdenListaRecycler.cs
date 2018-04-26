using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;

namespace ConsultasSP.Droid.Activities.PreOrden
{
    class PreOrdenListaRecycler : RecyclerView.Adapter
    {
        public List<OrdenViewModel> mListaOrden { get; private set; }
        public event EventHandler<PreOrdenListaRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PreOrdenListaRecyclerClickEventArgs> ItemLongClick;
        //string[] items;

        public PreOrdenListaRecycler(List<OrdenViewModel> ListaOrden)
        {
            mListaOrden = ListaOrden;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_preorden;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PreOrdenListaRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaOrden[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PreOrdenListaRecyclerViewHolder;
            holder.mCodigoOrden.Text = item.NROTRA.ToString();
            holder.mMontoOrden.Text = String.Format("S/. {0:0.00}", item.VALTOT);
            holder.mFechaOrden.Text = item.FECDOC.ToString("dd/MM/yyyy hh:mm tt");
            holder.mDescuentoOrden.Text = String.Format("Descuento: S/. {0:0.00}", 0);
            holder.mProveedorOrden.Text = item.DESPRV;
            holder.mUsuarioOrden.Text = item.DESEMP;
            holder.mSupervisorOrden.Text = String.Format("{0}", item.DESCOM);
            holder.mCondicionOrden.Text = item.DESCON;
        }

        public override int ItemCount => mListaOrden.Count;

        void OnClick(PreOrdenListaRecyclerClickEventArgs args) => PreordenDetalle(args);

        private void PreordenDetalle(PreOrdenListaRecyclerClickEventArgs args)
        {
            Intent intent = new Intent(args.View.Context, typeof(PreOrdenDetalleActivity));
            intent.PutExtra(PreOrdenDetalleActivity.intent_PreordenDetalle, JsonConvert.SerializeObject(mListaOrden[args.Position]));
            args.View.Context.StartActivity(intent);
        }

        void OnLongClick(PreOrdenListaRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PreOrdenListaRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public View mIndicadorEstado { get; set; }
        public TextView mCodigoOrden { get; set; }
        public TextView mMontoOrden { get; set; }
        public TextView mDescuentoOrden { get; set; }
        public TextView mFechaOrden { get; set; }
        public TextView mProveedorOrden { get; set; }
        public TextView mUsuarioOrden { get; set; }
        public TextView mSupervisorOrden { get; set; }
        public TextView mCondicionOrden { get; set; }

        public PreOrdenListaRecyclerViewHolder(View itemView, Action<PreOrdenListaRecyclerClickEventArgs> clickListener,
                            Action<PreOrdenListaRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            mIndicadorEstado = itemView.FindViewById<View>(Resource.Id.IndicadorEstado);
            mCodigoOrden = itemView.FindViewById<TextView>(Resource.Id.CodigoOrden);
            mMontoOrden = itemView.FindViewById<TextView>(Resource.Id.MontoOrden);
            mDescuentoOrden = itemView.FindViewById<TextView>(Resource.Id.DescuentoOrden);
            mFechaOrden = itemView.FindViewById<TextView>(Resource.Id.FechaOrden);
            mProveedorOrden = itemView.FindViewById<TextView>(Resource.Id.ProveedorOrden);
            mUsuarioOrden = itemView.FindViewById<TextView>(Resource.Id.UsuarioOrden);
            mSupervisorOrden = itemView.FindViewById<TextView>(Resource.Id.SupervisorOrden);
            mCondicionOrden = itemView.FindViewById<TextView>(Resource.Id.CondicionOrden);

            itemView.Click += (sender, e) => clickListener(new PreOrdenListaRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PreOrdenListaRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PreOrdenListaRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}