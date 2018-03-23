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
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentsFragment : BaseFragment
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentsView;

        private TournamentViewModel ViewModel { get; set; }
        private AddDialogViewModel AddTournamentViewModel { get; set; }
        private TournamentsAdapter TournamentsAdapter { get; set; }
        private RecyclerView TournamentsRecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Singleton.Instance.TournamentViewModel();
            AddTournamentViewModel = Singleton.Instance.AddTournamentViewModel();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            TournamentsAdapter = new TournamentsAdapter(ViewModel.Tournaments);
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
            AlertDialog.Builder inputDialog = new AlertDialog.Builder(this.Activity);
            inputDialog.SetTitle("Add Tournament");

            EditText userInput = new EditText(Activity)
            {
                Hint = "Enter Tournament Name",
                Top = 5,
                Focusable = true,
                ShowSoftInputOnFocus = true
            };            
            inputDialog.SetView(userInput);
            
            inputDialog.SetPositiveButton("Add", (senderAlert, args) => {
                var newtournamentKey = AddTournamentViewModel.AddTournament(userInput.Text);
                var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
                detailActivity.PutExtra("TournamentId", newtournamentKey);
                StartActivity(detailActivity);
            });
            inputDialog.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this.Activity, "Canceled.", ToastLength.Short).Show();
            });
            inputDialog.Show();
        }

        private void OnItemClick(object sender, string tournamentId)
        {
            var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
            detailActivity.PutExtra("TournamentId", tournamentId);
            StartActivity(detailActivity);
        }

        protected override void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<Tournament> tournaments = ViewModel.Tournaments.Where(t => t.Name.ToLower().Contains(SearchEditText.Text.ToLower()));
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