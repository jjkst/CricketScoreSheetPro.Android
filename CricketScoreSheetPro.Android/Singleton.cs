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
using CricketScoreSheetPro.Core.ViewModels;
using Firebase.Database;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;

namespace CricketScoreSheetPro.Android
{
    public class Singleton
    {
        #region Singleton
        private static readonly Singleton instance = new Singleton();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Singleton

        public FirebaseClient _client { get; set; } = new FirebaseClient("https://xamarinfirebase-4a90e.firebaseio.com/");
        public string UniqueUserId { get; set; } = "UUID";

        #region Tournament
        private ITournamentService _tournamentService => new TournamentService(
            new TournamentRepository(_client, UniqueUserId), 
            new TournamentDetailRepository(_client));

        private TournamentsViewModel tournamentsViewModel;
        public TournamentsViewModel TournamentsViewModel
        {
            get
            {
                if (tournamentsViewModel == null)
                    tournamentsViewModel = new TournamentsViewModel(_tournamentService);                    
                return tournamentsViewModel;
            }
        }

        private TournamentDetailViewModel _tournamentDetailViewModel;
        public TournamentDetailViewModel TournamentDetailViewModel(string tournamentId)
        {

            if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != tournamentId)
                _tournamentDetailViewModel = new TournamentDetailViewModel(_tournamentService, tournamentId);
            return _tournamentDetailViewModel;
        }

        #endregion Tournament
    }
}