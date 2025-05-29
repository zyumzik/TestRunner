using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class EndGameView : UIView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _reviveButton;
        [SerializeField] private TMP_Text _scoreText;
        
        public event Action OnRestartButton;
        public event Action OnReviveButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _reviveButton.onClick.AddListener(OnReviveButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _reviveButton.onClick.RemoveListener(OnReviveButtonClicked);
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = "Your score: \n" + score;
        }
        
        private void OnRestartButtonClicked()
        {
            OnRestartButton?.Invoke();
        }

        private void OnReviveButtonClicked()
        {
            OnReviveButton?.Invoke();
        }
    }
}