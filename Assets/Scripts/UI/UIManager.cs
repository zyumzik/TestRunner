using System;
using System.Collections.Generic;
using Configurations;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIManager
    {
        private readonly UIConfiguration _uiConfiguration;
        private readonly DiContainer _container;
        private readonly Transform _rootUI;
        private UIView _currentView;
        
        private readonly Dictionary<Type, UIView> _instances = new();

        public UIManager(UIConfiguration uiConfiguration, DiContainer container, Transform rootUI)
        {
            _uiConfiguration = uiConfiguration;
            _container = container;
            _rootUI = rootUI;
        }

        public T GetUIView<T>() where T : UIView
        {
            if (!_instances.TryGetValue(typeof(T), out var view))
            {
                var pair = _uiConfiguration.GetPair<T>();
                var viewPrefab = pair.ViewPrefab;
                var presenterType = pair.PresenterType;
                
                view = _container.InstantiatePrefabForComponent<T>(viewPrefab, _rootUI);
                _container.Instantiate(presenterType, new[] { view });

                _instances[typeof(T)] = view;
            }
            
            return (T)view;
        }
        
        public void Show<T>(bool hideCurrentView = true) where T : UIView
        {
            var view = GetUIView<T>();
            
            if (hideCurrentView)
            {
                _currentView?.Hide();
                _currentView = view;
            }

            view.Show();
        }
        
        public void Hide<T>() where T : UIView
        {
            var view = GetUIView<T>();
            view.Hide();
        }

        public void HideAll()
        {
            foreach (var view in _instances)
            {
                view.Value.Hide();
            }
        }
    }
}