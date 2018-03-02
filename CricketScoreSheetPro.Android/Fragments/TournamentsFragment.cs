﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text;
using CricketScoreSheetPro.Core.Models;
using Android.Support.V7.Widget;
using CricketScoreSheetPro.Core.ViewModels;

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentsFragment : Fragment
    {
        private TournamentsViewModel ViewModel { get; set; }
        private EditText SearchTournamentEditText { get; set; }
        private TournamentsAdapter TournamentsAdapter;
        private RecyclerView TournamentsRecyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TournamentsView, container, false);

            SearchTournamentEditText = view.FindViewById<EditText>(Resource.Id.searchTournament);
            SearchTournamentEditText.TextChanged += SearchText_TextChanged;

            TournamentsAdapter = new TournamentsAdapter();
            TournamentsAdapter.ItemClick += OnItemClick;

            TournamentsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.tournamentsrecyclerview);
            TournamentsRecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);

            //var scrollview = view.FindViewById<ScrollView>(Resource.Id.batsmanStatsListScrollView);
            //scrollview.SmoothScrollingEnabled = true;
            //scrollview.SmoothScrollTo(0, 0);
            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.searchText)
            {
                if (SearchTournamentEditText.Visibility == ViewStates.Gone)
                    SearchTournamentEditText.Visibility = ViewStates.Visible;
                else SearchTournamentEditText.Visibility = ViewStates.Gone;
                return true;
            }
            return base.OnOptionsItemSelected(item);  
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<UserTournament> matchedTournaments = (from t in ViewModel.Tournaments
                                                        where t.TournamentName.ToLower().Contains(SearchTournamentEditText.Text.ToLower())
                                                        select t).OrderByDescending(d=>d.AddDate).ToList();
            TournamentsAdapter.UpdatedList(matchedTournaments);
            TournamentsRecyclerView.SetAdapter(matchedTournaments);
        }
    }
}