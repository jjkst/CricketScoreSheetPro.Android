using CricketScoreSheetPro.Core.Repositories.Implementations;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Services.Interfaces;
using CricketScoreSheetPro.Core.ViewModels;
using Firebase.Database;

namespace CricketScoreSheetPro.Android
{
    public class Singleton
    {
        #region Singleton
        private static readonly Singleton instance = new Singleton();
        private FirebaseClient _client { get; set; }

        private Singleton()
        {
            _client = new FirebaseClient("https://xamarinfirebase-4a90e.firebaseio.com/");
            _tournamentService = new TournamentService(new TournamentRepository(_client, UniqueUserId), new TournamentDetailRepository(_client));
        }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Singleton

        
        public string UniqueUserId { get; set; } = "UUID";

        #region Tournament
        private TournamentService _tournamentService { get; set; }

        private TournamentViewModel tournamentViewModel;
        public TournamentViewModel TournamentViewModel
        {
            get
            {
                if (tournamentViewModel == null)
                    tournamentViewModel = new TournamentViewModel(_tournamentService);                    
                return tournamentViewModel;
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