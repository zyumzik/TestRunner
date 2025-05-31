using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace FirebaseModule
{
    public class AuthManager : IInitializable
    { 
        public bool IsUserLoggedIn => _auth.CurrentUser != null;
        public string UserId => _auth.CurrentUser?.UserId;
        public string UserDisplayName => _auth.CurrentUser?.DisplayName;
        
        private FirebaseAuth _auth;
        private FirebaseUser _user;

        public event Action OnSilentLoginSuccess;
        public event Action OnSilentLoginFailed;
        public event Action OnLoginSuccess;
        public event Action OnLoginFailed;
        public event Action OnRegisterSuccess;
        public event Action OnRegisterFailed;
        
        public void Initialize()
        {
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += AuthOnStateChanged;
        }

        public void SilentLogin()
        {
            if (_auth.CurrentUser != null)
            {
                _user = _auth.CurrentUser;
                Debug.Log("Silent login success");
                OnSilentLoginSuccess?.Invoke();
            }
            else
            {
                Debug.Log("Silent login failed");
                OnSilentLoginFailed?.Invoke();
            }
        }

        public Task Register(string username, string email, string password)
        {
            return _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    OnRegisterFailed?.Invoke();
                    Debug.LogError("Registration failed");
                    return;
                }

                FirebaseUser newUser = task.Result.User;

                var profile = new UserProfile { DisplayName = username };
                newUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(profileTask =>
                {
                    if (profileTask.IsCanceled || profileTask.IsFaulted)
                    {
                        Debug.LogError("Registration failed");
                        OnRegisterFailed?.Invoke();
                        return;
                    }
                    
                    Debug.LogFormat("User registered successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                    OnRegisterSuccess?.Invoke();
                });
            });
        }

        public Task Login(string email, string password)
        {
            return _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    OnLoginFailed?.Invoke();
                    Debug.LogError("Logging in failed");
                    return;
                }

                FirebaseUser newUser = task.Result.User;
                OnLoginSuccess?.Invoke();
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
        }

        public void Logout()
        {
            _auth.SignOut();
            Debug.Log("User signed out");
        }
        
        private void AuthOnStateChanged(object sender, EventArgs e)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                if (!signedIn && _user != null)
                {
                    Debug.Log("User logged out");
                }
                _user = _auth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log("User logged in: " + _user.Email);
                }
            }
        }
    }
}