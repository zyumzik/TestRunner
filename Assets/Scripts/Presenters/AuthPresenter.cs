using FirebaseModule;
using GameStateManagerModule;
using UI.Views;

namespace Presenters
{
    public class AuthPresenter : IPresenter
    {
        private readonly AuthView _view;
        private readonly AuthManager _authManager;
        private readonly GameStateManager _gameStateManager;

        public AuthPresenter(AuthView view, AuthManager authManager, GameStateManager gameStateManager)
        {
            _view = view;
            _authManager = authManager;
            _gameStateManager = gameStateManager;

            _authManager.OnSilentLoginSuccess += OnSilentLoginSuccess;
            _authManager.OnSilentLoginFailed += OnSilentLoginFailed;
            _authManager.OnLoginSuccess += OnLoginSuccess;
            _authManager.OnLoginFailed += OnLoginFailed;
            _authManager.OnRegisterSuccess += OnRegisterSuccess;
            _authManager.OnRegisterFailed += OnRegisterFailed;
            
            _view.OnLogin += OnLogin;
            _view.OnRegister += OnRegister;
            
            _view.SetWaitingPanelActivity(true);
        }

        private void OnSilentLoginSuccess()
        {
            _view.SetWaitingPanelActivity(false);
            _gameStateManager.PrepareGame();
        }

        private void OnSilentLoginFailed()
        {
            _view.SetWaitingPanelActivity(false);
        }

        private void OnLoginSuccess()
        {
            _gameStateManager.PrepareGame();
        }

        private void OnLoginFailed()
        {
            _view.SetWaitingPanelActivity(false);
            _view.ShowErrorPanel();
        }

        private void OnRegisterSuccess()
        {
            _gameStateManager.PrepareGame();
        }

        private void OnRegisterFailed()
        {
            _view.SetWaitingPanelActivity(false);
            _view.ShowErrorPanel();
        }
        
        private void OnLogin(string email, string password)
        {
            _view.SetWaitingPanelActivity(true);
            _authManager.Login(email, password);
        }

        private void OnRegister(string email, string password)
        {
            _view.SetWaitingPanelActivity(true);
            _authManager.Register(email, password);
        }
    }
}