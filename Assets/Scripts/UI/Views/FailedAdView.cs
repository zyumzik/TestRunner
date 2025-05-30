using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class FailedAdView : UIView
    {
        [SerializeField] private Button _okButton;

        public event Action OnOkButton;
        
        private void OnEnable()
        {
            _okButton.onClick.AddListener(OnOkButtonClicked);
        }

        private void OnDisable()
        {
            _okButton.onClick.RemoveListener(OnOkButtonClicked);
        }

        private void OnOkButtonClicked()
        {
            OnOkButton?.Invoke();
        }
    }
}