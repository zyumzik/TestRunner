using System;
using System.Collections.Generic;

namespace Core.PoolModule
{

    public class ShuffledObjectPool<T> where T : class
    {
        private readonly List<T> _pool = new();
        private readonly Func<T> _createFunc;
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Action<T> _actionOnDestroy;
        private readonly int _defaultCapacity;
        private readonly int _maxSize;

        public int CountInactive => _pool.Count;

        public ShuffledObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            int defaultCapacity = 10,
            int maxSize = 100)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;
            _defaultCapacity = defaultCapacity;
            _maxSize = maxSize;
            
            FillPool();
        }

        public T Get()
        {
            T element;
            if (_pool.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, _pool.Count);
                element = _pool[index];
                _pool.RemoveAt(index);
            }
            else
            {
                element = _createFunc();
            }

            _actionOnGet?.Invoke(element);
            return element;
        }

        public void Release(T element)
        {
            if (_pool.Count < _maxSize)
            {
                _actionOnRelease?.Invoke(element);
                _pool.Add(element);
            }
            else
            {
                _actionOnDestroy?.Invoke(element);
            }
        }

        public void Clear()
        {
            foreach (var item in _pool)
            {
                _actionOnDestroy?.Invoke(item);
            }
            _pool.Clear();
        }

        public void Refill()
        {
            Clear();
            
            FillPool();
        }

        private void FillPool()
        {
            for (int i = 0; i < _defaultCapacity; i++)
            {
                var element = _createFunc();
                _actionOnRelease?.Invoke(element);
                _pool.Add(element);
            }
        }
    }

}