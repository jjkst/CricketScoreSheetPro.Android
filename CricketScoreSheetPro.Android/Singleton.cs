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
        private static readonly Singleton instance = new Singleton();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        private FirebaseClient _client { get; } = new FirebaseClient("https://xamarinfirebase-4a90e.firebaseio.com/");

        private IRepository<Tournament> _tournamentRepository => new TournamentRepository(_client);
        private IRepository<UserTournament> _usertournamentRepository => new UserTournamentRepository(_client, "");
        private ITournamentService _tournamentService => new TournamentService(_tournamentRepository, _usertournamentRepository);

        public string TournamentId {private get; set; }
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
        public TournamentDetailViewModel TournamentDetailViewModel
        {
            get
            {
                if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != TournamentId)
                    _tournamentDetailViewModel = new TournamentDetailViewModel(_tournamentService, TournamentId);
                return _tournamentDetailViewModel;
            }
        }
    }
}