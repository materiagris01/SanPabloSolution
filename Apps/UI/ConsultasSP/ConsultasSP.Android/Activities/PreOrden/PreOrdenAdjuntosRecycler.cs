using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using System.Collections.Generic;
using System.IO;
using Android.Content;

namespace ConsultasSP.Droid.Activities.PreOrden
{
    class PreOrdenAdjuntosRecycler : RecyclerView.Adapter
    {
        public List<OrdenAdjuntosViewModel> mListaAdjuntoPreorden { get; private set; }
        public event EventHandler<PreordenAdjuntosRecyclerClickEventArgs> ItemClick;
        public event EventHandler<PreordenAdjuntosRecyclerClickEventArgs> ItemLongClick;

        public PreOrdenAdjuntosRecycler(List<OrdenAdjuntosViewModel> ListaAdjuntoPreorden)
        {
            mListaAdjuntoPreorden = ListaAdjuntoPreorden;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_preorden_adjuntos;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PreordenAdjuntosRecyclerViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = mListaAdjuntoPreorden[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PreordenAdjuntosRecyclerViewHolder;
            holder.mNombrePdfPreorden.Text = String.Format("{0}.{1}", item.DESADJ, item.FORMAT);
            holder.mFechaPdfPreorden.Text = DateTime.Now.ToString();
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => mListaAdjuntoPreorden.Count;

        void OnClick(PreordenAdjuntosRecyclerClickEventArgs args) => PreordenAdjunto(args);

        private void PreordenAdjunto(PreordenAdjuntosRecyclerClickEventArgs args)
        {
            var Name = "Download";
            byte[] byteArray = Convert.FromBase64String(mListaAdjuntoPreorden[args.Position].CONTEN);
            var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            directory = Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
            string filePath = Path.Combine(directory.ToString(), Name);
            File.WriteAllBytes(filePath, byteArray);

            Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(pdfPath, "application/pdf");
            intent.SetFlags(ActivityFlags.NewTask);
            args.View.Context.StartActivity(intent);
        }

        void OnLongClick(PreordenAdjuntosRecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PreordenAdjuntosRecyclerViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public View mView { get; set; }
        public TextView mNombrePdfPreorden { get; set; }
        public TextView mFechaPdfPreorden { get; set; }

        public PreordenAdjuntosRecyclerViewHolder(View itemView, Action<PreordenAdjuntosRecyclerClickEventArgs> clickListener,
                            Action<PreordenAdjuntosRecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            mNombrePdfPreorden = itemView.FindViewById<TextView>(Resource.Id.NombrePdfPreorden);
            mFechaPdfPreorden = itemView.FindViewById<TextView>(Resource.Id.FechaPdfPreorden);
            itemView.Click += (sender, e) => clickListener(new PreordenAdjuntosRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PreordenAdjuntosRecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PreordenAdjuntosRecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}