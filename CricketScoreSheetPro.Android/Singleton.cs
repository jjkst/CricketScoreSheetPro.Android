using System;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Services.Interfaces;
using CricketScoreSheetPro.Core.ViewModels;
using Firebase.Database;

namespace CricketScoreSheetPro.Android
{
    public class Singleton
    {
        private FirebaseClient _client { get; set; }
        private string FirebaseURL { get; set; } = "https://cricket-score-sheet.firebaseio.com";
        public string UniqueUserId { get; set; } = "UUID";

        #region Singleton

        private static readonly Singleton instance = new Singleton();
        
        private Singleton()
        {
            _client = new FirebaseClient(FirebaseURL);            
        }

        public static Singleton Instance => instance;

        #endregion Singleton                

        #region Tournament

        private TournamentService tournamentService;
        private TournamentService TournamentService()
        {
            if (tournamentService == null)
                tournamentService = new TournamentService(new TournamentRepository(_client, UniqueUserId), new TournamentDetailRepository(_client));
            return tournamentService;
        }

        private AddDialogViewModel addTournamentViewModel;
        public AddDialogViewModel AddTournamentViewModel()
        {
            addTournamentViewModel = addTournamentViewModel ?? new AddDialogViewModel(tournamentService);
            return addTournamentViewModel;
        }

        private TournamentViewModel tournamentViewModel;
        public TournamentViewModel TournamentViewModel()
        {
            tournamentViewModel = tournamentViewModel ?? new TournamentViewModel(tournamentService);
            return tournamentViewModel;
        }

        private TournamentDetailViewModel _tournamentDetailViewModel;
        public TournamentDetailViewModel TournamentDetailViewModel(string tournamentId)
        {

            if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != tournamentId)
                _tournamentDetailViewModel = new TournamentDetailViewModel(tournamentService, tournamentId);
            return _tournamentDetailViewModel;
        }

        #endregion Tournament
    }
}