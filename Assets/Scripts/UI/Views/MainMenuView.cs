using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : UIView
    {
        [SerializeField] private Button _startButton;
        
        public event Action OnStartButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => OnStartButton?.Invoke());
        }
    }
}