using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class AuthView : UIView
    {
        [SerializeField] private GameObject _waitingPanel;
        [SerializeField] private GameObject _errorPanel;
        [SerializeField] private Button _errorPanelButton;
        
        [SerializeField] private Button _startLoginButton;
        [SerializeField] private Button _startRegisterButton;
        
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private TMP_InputField _loginEmailInputField;
        [SerializeField] private TMP_InputField _loginPasswordInputField;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _loginPanelBackButton;
        
        [SerializeField] private GameObject _registerPanel;
        [SerializeField] private TMP_InputField _registerUsernameInputField;
        [SerializeField] private TMP_InputField _registerEmailInputField;
        [SerializeField] private TMP_InputField _registerPasswordInputField;
        [SerializeField] private TMP_InputField _registerPasswordConfirmInputField;
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _registerPanelBackButton;

        public event Action<string, string> OnLogin;
        public event Action<string, string, string> OnRegister;

        private void OnEnable()
        {
            _loginPanel.SetActive(false);
            _registerPanel.SetActive(false);
            
            _errorPanelButton.onClick.AddListener(OnPanelButtonClicked);
            _startLoginButton.onClick.AddListener(OnStartLoginClicked);
            _startRegisterButton.onClick.AddListener(OnStartRegisterClicked);
            _loginButton.onClick.AddListener(OnLoginClicked);
            _registerButton.onClick.AddListener(OnRegisterClicked);
            _loginPanelBackButton.onClick.AddListener(OnLoginPanelBackClicked);
            _registerPanelBackButton.onClick.AddListener(OnRegisterPanelBackClicked);
        }

        private void OnDisable()
        {
            _errorPanelButton.onClick.RemoveListener(OnPanelButtonClicked);
            _startLoginButton.onClick.RemoveListener(OnStartLoginClicked);
            _startRegisterButton.onClick.RemoveListener(OnStartRegisterClicked);
            _loginButton.onClick.RemoveListener(OnLoginClicked);
            _registerButton.onClick.RemoveListener(OnRegisterClicked);
        }

        public void SetWaitingPanelActivity(bool value)
        {
            _waitingPanel.SetActive(value);
            //Debug.Log($"SetWaitingPanelActivity: {value}");
        }

        public void ShowErrorPanel()
        {
            _errorPanel.SetActive(true);
        }

        private void OnPanelButtonClicked()
        {
            _errorPanel.SetActive(false);
        }
        
        private void OnStartLoginClicked()
        {
            _loginPanel.SetActive(true);
        }

        private void OnStartRegisterClicked()
        {
            _registerPanel.SetActive(true);
        }

        private void OnLoginClicked()
        {
            OnLogin?.Invoke(_loginEmailInputField.text, _loginPasswordInputField.text);
        }

        private void OnRegisterClicked()
        {
            if (String.CompareOrdinal(_registerPasswordInputField.text, _registerPasswordConfirmInputField.text) == 0)
            {
                OnRegister?.Invoke(_registerUsernameInputField.text, _registerEmailInputField.text,
                    _registerPasswordInputField.text);
            }
            else
            {
                _registerPasswordInputField.text = "";
                _registerPasswordConfirmInputField.text = "";
            }
        }

        private void OnLoginPanelBackClicked()
        {
            _loginPanel.SetActive(false);
        }

        private void OnRegisterPanelBackClicked()
        {
            _registerPanel.SetActive(false);
        }
    }
}