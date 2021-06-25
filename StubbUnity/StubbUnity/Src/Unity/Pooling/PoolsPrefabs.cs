using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StubbUnity.Unity.Pooling;
using UnityEngine;

namespace StubbUnity.StubbFramework.Pooling
{
    public partial class Pools
    {
        private readonly Dictionary<GameObject, IPool<GameObject>> _prefabPools = new Dictionary<GameObject, IPool<GameObject>>();

        public int NumPools => _pools.Count + _prefabPools.Count;

        public void ClearPrefabPools()
        {
            foreach (var pair in _prefabPools)
            {
                pair.Value.Clear();
            }
        }

        public void DisposePrefabPools()
        {
            foreach (var pair in _prefabPools)
            {
                pair.Value.OnRemove -= OnRemovePoolHandler;
                pair.Value.Dispose();
            }

            _prefabPools.Clear();
        }
        
        public bool Has(GameObject prefab)
        {
            return _prefabPools.ContainsKey(prefab);
        }

        /// <summary>
        /// Returns pool by prefab. 
        /// </summary>
        public IPool<GameObject> Get(GameObject prefab, bool prewarm = false)
        {
            return Get(prefab, 128, null, prewarm);
        }
        
        /// <summary>
        /// Returns pool by prefab. 
        /// </summary>
        public IPool<GameObject> Get(GameObject prefab, int capacity = 128, bool prewarm = false)
        {
            return Get(prefab, capacity, null, prewarm);
        }
        
        /// <summary>
        /// Returns pool by prefab. 
        /// </summary>
        public IPool<GameObject> Get(GameObject prefab, int capacity = 128, ICreator<GameObject> creator = null, bool prewarm = false)
        {
            if (Has(prefab))
                return _prefabPools[prefab];

            var pool = _CreatePrefabPool(prefab, capacity);
            pool.Creator = creator ?? new CreatorGameObject(prefab, pool);

            if (prewarm)
                pool.PreWarm();
            
            return pool;
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IPool<GameObject> _CreatePrefabPool(GameObject prefab, int capacity)
        {
            var pool = new Pool<GameObject>(capacity);
            pool.OnRemove += OnRemovePoolHandler;
            _prefabPools[prefab] = pool;
            return pool;
        }
    }
}