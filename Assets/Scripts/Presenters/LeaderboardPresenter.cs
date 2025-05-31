using FirebaseModule;
using UI.Views;

namespace Presenters
{
    public class LeaderboardPresenter : IPresenter
    {
        private readonly LeaderboardView _view;
        private readonly LeaderboardManager _leaderboardManager;

        public LeaderboardPresenter(LeaderboardView view, LeaderboardManager leaderboardManager)
        {
            _view = view;
            _leaderboardManager = leaderboardManager;
            view.OnShow += OnShow;
        }

        private async void OnShow()
        {
            var results = await _leaderboardManager.GetTopScores(_view.TopResultsCount); 
            if (results != null) _view.UpdateTopResults(results);
            var userData = await _leaderboardManager.GetUserScore();
            if (userData != null)_view.UpdateUserResults(userData);
        }
    }
}