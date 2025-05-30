using System;
using FirebaseModule;
using PlayerModule;
using UI;
using UI.Views;
using Zenject;

namespace GameStateManagerModule
{
    public class GameStateManager : IInitializable
    {
        private readonly UIManager _uiManager;
        private readonly AuthManager _authManager;
        private readonly Player _player;

        public event Action OnGamePrepared;
        public event Action OnGameStart;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameEnd;
        public event Action OnGameRestarted; 
        
        public GameStateManager(UIManager uiManager, AuthManager authManager)
        {
            _uiManager = uiManager;
            _authManager = authManager;
        }

        public void Initialize()
        {
            _uiManager.Show<AuthView>();
            _authManager.SilentLogin();
        }

        public void PrepareGame()
        {
            _uiManager.Show<MainMenuView>();
            OnGamePrepared?.Invoke();
        }
        
        public void StartGame()
        {
            _uiManager.Show<GameplayView>();
            
            OnGameStart?.Invoke();
        }

        public void PauseGame() => OnGamePaused?.Invoke();

        public void ResumeGame() => OnGameResumed?.Invoke();

        public void EndGame()
        {
            _uiManager.Show<EndGameView>();
            
            OnGameEnd?.Invoke();
        }

        /// <summary>
        /// Restarts game and reviving player
        /// </summary>
        public void RestartGame()
        {
            _uiManager.Show<GameplayView>();
            
            OnGameRestarted?.Invoke();
        }
    }
}