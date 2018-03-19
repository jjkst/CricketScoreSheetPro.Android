using Android.App;
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

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentsFragment : Fragment
    {
        private TournamentViewModel ViewModel { get; set; }
        private EditText SearchTournamentEditText { get; set; }
        private TournamentsAdapter TournamentsAdapter;
        private RecyclerView TournamentsRecyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            base.OnCreate(savedInstanceState);
            ViewModel = Singleton.Instance.TournamentViewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TournamentsView, container, false);

            SearchTournamentEditText = view.FindViewById<EditText>(Resource.Id.searchTournament);
            SearchTournamentEditText.TextChanged += SearchText_TextChanged;

            TournamentsAdapter = new TournamentsAdapter(ViewModel.Tournaments);
            //TournamentsAdapter = new TournamentsAdapter(
            //    new List<Tournament> {
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "1",
            //        Name = "Tournament Name one",
            //        AddDate = DateTime.Today,
            //        Status = "Open"
            //        },
            //        new Tournament {
            //        Id =  "2",
            //        Name = "Tournament Name two",
            //        AddDate = DateTime.Today,
            //        Status = "Open",
            //        ImportedFlg = true
            //        },
            //    });
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
            TournamentsAdapter.FilterTournaments(SearchTournamentEditText.Text.ToLower());
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);
        }

        private void OnItemClick(object sender, string tournamentId)
        {
            var detailActivity = new Intent(this.Activity, typeof(TournamentDetailActivity));
            detailActivity.PutExtra("TournamentId", tournamentId);
            StartActivity(detailActivity);
        }
    }
}