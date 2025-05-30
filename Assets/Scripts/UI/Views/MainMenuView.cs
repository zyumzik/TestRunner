using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : UIView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _logoutButton;
        
        public event Action OnStartButton;
        public event Action OnLogoutButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => OnStartButton?.Invoke());
            _logoutButton.onClick.AddListener(() => OnLogoutButton?.Invoke());
        }
    }
}