using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ApplicationTweaks : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;
        
        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
            QualitySettings.vSyncCount = 0;
        }
    }
}