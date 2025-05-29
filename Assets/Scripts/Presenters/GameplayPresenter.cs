using GameStateManagerModule;
using RoadSystem;
using UI.Views;

namespace Presenters
{
    public class GameplayPresenter : IPresenter
    {
        public GameplayPresenter(GameplayView view, RoadScoreCounter roadScoreCounter, 
            GameStateManager gameStateManager)
        {
            view.OnPauseButton += gameStateManager.PauseGame;
            view.OnResumeButton += gameStateManager.ResumeGame; 
            roadScoreCounter.OnTimeScoreChanged += view.UpdateScoreText;
        }
    }
}