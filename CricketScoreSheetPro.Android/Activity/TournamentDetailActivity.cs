using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using CricketScoreSheetPro.Core.ViewModels;
using Android.Support.V7.Widget;

namespace CricketScoreSheetPro.Android.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TournamentDetailView);

            // Initialize toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.TournamentDetailActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var tournamentId = Intent.GetStringExtra("TournamentId");
            Singleton.Instance.TournamentId = tournamentId;
            SetTournament();
        }

        private void SetTournament()
        {            
            //var vm = Singleton.Instance.TournamentDetailViewModel;
        }
    }
}