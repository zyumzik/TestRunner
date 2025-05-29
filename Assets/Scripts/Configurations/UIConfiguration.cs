using System;
using System.Collections.Generic;
using Core.SerializableTypeModule;
using Presenters;
using UI;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/UI Configuration")]
    public class UIConfiguration : ScriptableObject
    {
        [SerializeField] private List<ViewPresenterPair> _viewPrefabs;

        private Dictionary<Type, ViewPresenterPair> _cachedMap;
        
        public ViewPresenterPair GetPair<T>() where T : UIView
        {
            _cachedMap ??= CacheMap();
            
            if (_cachedMap.TryGetValue(typeof(T), out var pair))
                return pair;

            throw new Exception($"No pair found for view type {typeof(T)}");
        }

        private Dictionary<Type, ViewPresenterPair> CacheMap()
        {
            var map = new Dictionary<Type, ViewPresenterPair>();
            foreach (var pair in _viewPrefabs)
            {
                map[pair.ViewPrefab.GetType()] = pair;
            }
            return map;
        }
        
        [Serializable]
        public class ViewPresenterPair
        {
            [SerializeField] private UIView _viewPrefab;
            
            [TypeFilter(typeof(IPresenter))]
            [SerializeField] private SerializableType _presenterType;
            
            public UIView ViewPrefab => _viewPrefab;
            public Type PresenterType => _presenterType.Type;
        }
    }
}