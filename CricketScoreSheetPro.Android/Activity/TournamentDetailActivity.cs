﻿using System;
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
using Android.Widget;

namespace CricketScoreSheetPro.Android.Activity
{
    [Activity(Label = "Tournament Detail", Theme = "@style/MyTheme")]
    public class TournamentDetailActivity : BaseActivity
    {
        protected override int GetLayoutResourceId => Resource.Layout.TournamentDetailView;

        private TournamentDetailViewModel ViewModel { get; set; }
        private TextView Name { get; set; }
        private TextView Sponsor { get; set; }
        private TextView Status { get; set; }
        private TextView StartDate { get; set; }
        private TextView EntryFee { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var tournamentId = Intent.GetStringExtra("TournamentId");
            ViewModel = Singleton.Instance.TournamentDetailViewModel(tournamentId);
            Name = (TextView)FindViewById(Resource.Id.NameValue);
            Sponsor = (TextView)FindViewById(Resource.Id.SponsorValue);
            Status = (TextView)FindViewById(Resource.Id.StatusValue);
            StartDate = (TextView)FindViewById(Resource.Id.StartDateValue);
            EntryFee = (TextView)FindViewById(Resource.Id.EntryFeeValue);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Name.Text = ViewModel.Tournament.Name;
            Sponsor.Text = ViewModel.Tournament.Sponsor;
            Status.Text = ViewModel.Tournament.Status;
            StartDate.Text = ViewModel.Tournament.StartDate.ToShortDateString();
            EntryFee.Text = ViewModel.Tournament.EntryFee.ToString();
        }

    }
}