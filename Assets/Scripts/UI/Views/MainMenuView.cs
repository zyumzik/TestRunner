using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : UIView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _logoutButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private TMP_Text _displayNameText;
        
        public event Action OnStartButton;
        public event Action OnLogoutButton;
        public event Action OnLeaderboardButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => OnStartButton?.Invoke());
            _logoutButton.onClick.AddListener(() => OnLogoutButton?.Invoke());
            _leaderboardButton.onClick.AddListener(() => OnLeaderboardButton?.Invoke());
        }

        public void UpdateDisplayName(string displayName)
        {
            _displayNameText.text = displayName;
        }
    }
}