using System.Threading.Tasks;
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
            
            _view.OnLogin += OnViewLogin;
            _view.OnRegister += OnViewRegister;
            
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
            _view.SetWaitingPanelActivity(false);
            _gameStateManager.PrepareGame();
        }

        private void OnLoginFailed()
        {
            _view.SetWaitingPanelActivity(false);
            _view.ShowErrorPanel();
        }

        private void OnRegisterSuccess()
        {
            _view.SetWaitingPanelActivity(false);
            _gameStateManager.PrepareGame();
        }

        private void OnRegisterFailed()
        {
            _view.SetWaitingPanelActivity(false);
            _view.ShowErrorPanel();
        }
        
        private async void OnViewLogin(string email, string password)
        {
            _view.SetWaitingPanelActivity(true);
            await _authManager.Login(email, password);
        }

        private async void OnViewRegister(string username, string email, string password)
        {
            _view.SetWaitingPanelActivity(true);
            await _authManager.Register(username, email, password);
        }
    }
}