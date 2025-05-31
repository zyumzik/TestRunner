using System;
using UnityEngine;

namespace UI
{
    public class UIView : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnHide;
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShow?.Invoke();
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }
}