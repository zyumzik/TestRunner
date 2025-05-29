using GameStateManagerModule;
using RoadSystem;
using UI.Views;

namespace Presenters
{
    public class EndGamePresenter : IPresenter
    {
        private readonly EndGameView _view;
        private readonly GameStateManager _gameStateManager;

        public EndGamePresenter(EndGameView view, GameStateManager gameStateManager, RoadScoreCounter roadScoreCounter)
        {
            _view = view;
            _gameStateManager = gameStateManager;

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
            _gameStateManager.RestartGame();  // temp
        }
    }
}