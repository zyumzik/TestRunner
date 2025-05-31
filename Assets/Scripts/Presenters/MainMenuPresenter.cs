using FirebaseModule;
using GameStateManagerModule;
using UI;
using UI.Views;

namespace Presenters
{
    public class MainMenuPresenter : IPresenter
    {
        public MainMenuPresenter(MainMenuView view, GameStateManager gameStateManager, AuthManager authManager,
            UIManager uiManager)
        {
            view.OnStartButton += gameStateManager.StartGame;
            view.OnLogoutButton += () => { authManager.Logout(); uiManager.Show<AuthView>(); };
            view.OnLeaderboardButton +=  () => { uiManager.Show<LeaderboardView>(false); };

            view.OnShow += () => { view.UpdateDisplayName(authManager.UserDisplayName); };
            
            authManager.OnLoginSuccess += () => { view.UpdateDisplayName(authManager.UserDisplayName); };
            authManager.OnRegisterSuccess += () => { view.UpdateDisplayName(authManager.UserDisplayName); };
            authManager.OnSilentLoginSuccess += () => { view.UpdateDisplayName(authManager.UserDisplayName); };
        }
    }
}