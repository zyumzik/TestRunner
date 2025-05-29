using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameplayView : UIView
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Transform _pausePanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;

        public event Action OnPauseButton;
        public event Action OnResumeButton; 

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        }
        
        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            _pausePanel.gameObject.SetActive(true);
            OnPauseButton?.Invoke();
        }

        private void OnResumeButtonClicked()
        {
            _pausePanel.gameObject.SetActive(false);
            OnResumeButton?.Invoke();
        }
        
        public void UpdateScoreText(int score)
        {
            _scoreText.text = $"Score:\n{score}";
        }
    }
}