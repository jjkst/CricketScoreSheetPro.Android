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
using Android.Text;
using CricketScoreSheetPro.Core.Models;
using Android.Support.V7.Widget;
using CricketScoreSheetPro.Core.ViewModels;
using CricketScoreSheetPro.Android.Adapters;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using CricketScoreSheetPro.Android.Activity;

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
            //ViewModel = Singleton.Instance.TournamentsViewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TournamentsView, container, false);

            SearchTournamentEditText = view.FindViewById<EditText>(Resource.Id.searchTournament);
            SearchTournamentEditText.TextChanged += SearchText_TextChanged;

            //TournamentsAdapter = new TournamentsAdapter(ViewModel.Tournaments);
            TournamentsAdapter = new TournamentsAdapter(
                new List<Tournament> {
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
                    Status = "Open"
                    },
                });
            TournamentsAdapter.ItemClick += OnItemClick;

            TournamentsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.tournamentsrecyclerview);
            TournamentsRecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            TournamentsRecyclerView.SetAdapter(TournamentsAdapter);

            //IsRecyclerScrollable(TournamentsRecyclerView);
          
            return view;
        }

        public bool IsRecyclerScrollable(RecyclerView recyclerView)
        {
            return recyclerView.ComputeHorizontalScrollRange() > recyclerView.Width
                || recyclerView.ComputeVerticalScrollRange() > recyclerView.Height;
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