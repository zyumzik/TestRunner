using FirebaseModule;
using GameStateManagerModule;
using UI;
using UI.Views;

namespace Presenters
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly AuthManager _authManager;

        public MainMenuPresenter(MainMenuView mainMenuView, GameStateManager gameStateManager, AuthManager authManager,
            UIManager uiManager)
        {
            _authManager = authManager;
            mainMenuView.OnStartButton += gameStateManager.StartGame;
            mainMenuView.OnLogoutButton += () => { _authManager.Logout(); uiManager.Show<AuthView>(); };
        }
    }
}