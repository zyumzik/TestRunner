using GameStateManagerModule;
using UI;
using UI.Views;

namespace Presenters
{
    public class FailedAdPresenter : IPresenter
    {
        private readonly FailedAdView _view;
        private readonly UIManager _uiManager;
        private readonly GameStateManager _gameStateManager;

        public FailedAdPresenter(FailedAdView view, UIManager uiManager, GameStateManager gameStateManager)
        {
            _view = view;
            _uiManager = uiManager;
            _gameStateManager = gameStateManager;

            view.OnOkButton += OnOkButton;
        }

        private void OnOkButton()
        {
            _uiManager.HideAll();
            _gameStateManager.PrepareGame();
        }
    }
}