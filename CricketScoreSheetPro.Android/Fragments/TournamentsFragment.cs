﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using CricketScoreSheetPro.Android.Activity;
using CricketScoreSheetPro.Android.Adapters;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentsFragment : BaseFragment
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentsView;

        private TournamentsViewModel ViewModel { get; set; }   
        private TournamentsAdapter TournamentsAdapter { get; set; }
        private RecyclerView TournamentsRecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //ViewModel = Singleton.Instance.TournamentViewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            TournamentsAdapter = new TournamentsAdapter(DummyList());
            TournamentsAdapter.ItemClick += OnItemClick;

            FloatingActionButton addTournament = view.FindViewById<FloatingActionButton>(Resource.Id.floating_action_button_fab_with_listview);
            addTournament.Click += ShowAddTournamentDialog;

            TournamentsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.tournamentsrecyclerview);
            var layoutManager = new LinearLayoutManager(this.Activity);
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(layoutManager)
            {
                FloatingButton = addTournament
            };
            TournamentsRecyclerView.AddOnScrollListener(onScrollListener);
            TournamentsRecyclerView.SetLayoutManager(layoutManager);           
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
            return view;
        }

        private void ShowAddTournamentDialog(object sender, EventArgs e)
        {
            
        }

        private void OnItemClick(object sender, string tournamentId)
        {
            var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
            detailActivity.PutExtra("TournamentId", tournamentId);
            StartActivity(detailActivity);
        }

        protected override void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<Tournament> tournaments = DummyList().Where(t => t.Name.ToLower().Contains(SearchEditText.Text.ToLower()));
            TournamentsAdapter.RefreshTournaments(tournaments);
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        private List<Tournament> DummyList()
        {
            return new List<Tournament> {
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "1",
                    Name = "Tournament Name one",
                    AddDate = DateTime.Today,
                    Status = "Open"
                    },
                    new Tournament {
                    Id =  "2",
                    Name = "Tournament Name two",
                    AddDate = DateTime.Today,
                    Status = "Open",
                    ImportedFlg = true
                    }
            };
        }        
    }
}