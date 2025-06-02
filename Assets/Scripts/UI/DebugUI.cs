using AClockworkBerry;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private ScreenLogger _screenLogger;
        [SerializeField] private Button _consoleButton;

        private void OnEnable()
        {
            _consoleButton.onClick.AddListener(SwitchConsoleActivity);
        }

        private void OnDisable()
        {
            _consoleButton.onClick.RemoveListener(SwitchConsoleActivity);
        }

        private void SwitchConsoleActivity()
        {
            _screenLogger.ShowLog = !_screenLogger.ShowLog;
        }
    }
}