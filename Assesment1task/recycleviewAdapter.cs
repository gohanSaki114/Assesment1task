
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace Assesment1task
{
    internal class recycleviewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<recycleviewAdapterClickEventArgs> ItemClick;
        public event EventHandler<recycleviewAdapterClickEventArgs> ItemLongClick;
        List<listmodel> items;
        parkingactivity con;

        public recycleviewAdapter(System.Collections.Generic.List<listmodel> data, AndroidX.Fragment.App.FragmentActivity activity)
        {
            items = data;
            con =(parkingactivity)activity;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.listtrowitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new recycleviewAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
        var holder = viewHolder as recycleviewAdapterViewHolder;
           holder.dateTextView.Text = item.dateTime.ToString();
            holder.locationTextView.Text = item.locationName.ToString();

        }

        public override int ItemCount => items.Count;

        void OnClick(recycleviewAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(recycleviewAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class recycleviewAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
      public TextView dateTextView, locationTextView;

        public recycleviewAdapterViewHolder(View itemView, Action<recycleviewAdapterClickEventArgs> clickListener,
                            Action<recycleviewAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            dateTextView = itemView.FindViewById<TextView>(Resource.Id.dateTextview);
            locationTextView = itemView.FindViewById<TextView>(Resource.Id.locationTextview);   
            itemView.Click += (sender, e) => clickListener(new recycleviewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new recycleviewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class recycleviewAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
//mail": "eve.holt@reqres.in",
// pistol