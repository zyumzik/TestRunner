using GameStateManagerModule;
using UI.Views;

namespace Presenters
{
    public class MainMenuPresenter : IPresenter
    {
        public MainMenuPresenter(MainMenuView mainMenuView, GameStateManager gameStateManager)
        {
            mainMenuView.OnStartButton += gameStateManager.StartGame;
        }
    }
}