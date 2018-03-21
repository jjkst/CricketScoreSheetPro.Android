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

        public TournamentService SetTournamentService;
        private TournamentService TournamentService()
        {
            if (SetTournamentService == null)
                SetTournamentService = new TournamentService(new TournamentRepository(_client, UniqueUserId), new TournamentDetailRepository(_client));
            return SetTournamentService;
        }

        private TournamentViewModel tournamentViewModel;
        public TournamentViewModel TournamentViewModel()
        {
            tournamentViewModel = tournamentViewModel ?? new TournamentViewModel(TournamentService());
            return tournamentViewModel;
        }

        private TournamentDetailViewModel _tournamentDetailViewModel;
        public TournamentDetailViewModel TournamentDetailViewModel(string tournamentId)
        {

            if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != tournamentId)
                _tournamentDetailViewModel = new TournamentDetailViewModel(TournamentService(), tournamentId);
            return _tournamentDetailViewModel;
        }

        #endregion Tournament
    }
}