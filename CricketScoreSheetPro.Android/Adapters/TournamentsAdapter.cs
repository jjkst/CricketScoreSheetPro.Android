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
using Android.Support.V7.Widget;
using CricketScoreSheetPro.Core.Models;
using Android.Support.V4.Content;
using Android.Graphics;

namespace CricketScoreSheetPro.Android.Adapters
{
    public class TournamentsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<string> ItemClick;
        private List<Tournament> _tournaments;

        public TournamentsAdapter(List<Tournament> tournaments)
        {
            _tournaments = tournaments.OrderByDescending(d => d.AddDate).ToList();
        }

        public override int ItemCount => _tournaments.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TournamentViewHolder vh = holder as TournamentViewHolder;

            vh?.ItemView.SetBackgroundResource(position % 2 == 1
                            ? Resource.Drawable.listview_selector_even
                            : Resource.Drawable.listview_selector_odd);

            vh.Name.Text = _tournaments[position].Name;
            vh.Status.Text = _tournaments[position].Status;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.TournamentRow, parent, false);
            return new TournamentViewHolder(itemView, OnClick);
        }

        public void FilterTournaments(string searchtext)
        {
            _tournaments = _tournaments.Where(t => t.Name.ToLower().Contains(searchtext))
                .OrderByDescending(t => t.AddDate).ToList();
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, _tournaments[position].Id);
        }
    }

    public class TournamentViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Status { get; private set; }

        public TournamentViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.tournamentname);
            Status = itemView.FindViewById<TextView>(Resource.Id.tournamentstatus);
            itemView.Click += (sender, e) => listener(AdapterPosition);
        }
    }
}