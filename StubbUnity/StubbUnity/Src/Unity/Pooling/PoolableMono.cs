using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    public sealed class PoolableMono : MonoBehaviour
    {
        private IPoolable[] _poolingItems;

        private void Awake()
        {
            _poolingItems = GetComponentsInChildren<IPoolable>(true);
        }

        public void OnToPool()
        {
            if (_poolingItems == null) return;

            foreach (var poolingItem in _poolingItems)
                poolingItem.OnToPool();
        }

        public void OnFromPool()
        {
            if (_poolingItems == null) return;

            foreach (var poolingItem in _poolingItems)
                poolingItem.OnFromPool();
        }
    }
}