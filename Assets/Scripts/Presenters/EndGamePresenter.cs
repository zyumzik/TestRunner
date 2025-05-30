using AdsModule;
using GameStateManagerModule;
using RoadSystem;
using UI;
using UI.Views;

namespace Presenters
{
    public class EndGamePresenter : IPresenter
    {
        private readonly EndGameView _view;
        private readonly GameStateManager _gameStateManager;
        private readonly AdsManager _adsManager;
        private readonly UIManager _uiManager;

        public EndGamePresenter(EndGameView view, GameStateManager gameStateManager, RoadScoreCounter roadScoreCounter,
            AdsManager adsManager, UIManager uiManager)
        {
            _view = view;
            _gameStateManager = gameStateManager;
            _adsManager = adsManager;
            _uiManager = uiManager;

            roadScoreCounter.OnTimeScoreChanged += _view.SetScoreText;
            _view.OnRestartButton += OnRestart;
            _view.OnReviveButton += OnRevive;
        }

        private void OnRestart()
        {
            _gameStateManager.PrepareGame();
        }

        private void OnRevive()
        {
            _adsManager.ShowRewardedAd(OnRewarded, OnFailed);
        }

        private void OnRewarded()
        {
            _gameStateManager.RestartGame();
        }

        private void OnFailed()
        {
            _uiManager.Show<FailedAdView>(false);
        }
    }
}