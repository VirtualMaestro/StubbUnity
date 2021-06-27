using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StubbUnity.StubbFramework.Pooling
{
    public partial class Pools
    {
        #region Singleton

        private static Pools _instance;
        public static Pools I => _instance ?? (_instance = new Pools());

        #endregion

        private readonly Dictionary<Type, IPoolGeneric> _pools;

        private Pools()
        {
            _pools = new Dictionary<Type, IPoolGeneric>();
        }

        /// <summary>
        /// Get or creates and get a pool with given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity = 128, bool prewarm = false)
        {
            if (!_GetPool<T>(out var pool))
                pool = _Register(new Pool<T>(capacity, prewarm));

            return pool;
        }

        /// <summary>
        /// Get or creates and get a pool with given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool.</param>
        /// <param name="creator">Instance of ICreator which will be used for creating an instance of pool's type.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity, ICreator<T> creator, bool prewarm = false)
        {
            if (!_GetPool<T>(out var pool))
                pool = _Register(new Pool<T>(capacity, creator, prewarm));

            return pool;
        }

        /// <summary>
        /// Get or creates and get a pool with given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool.</param>
        /// <param name="createMethod">Method which will be used for creating an instance of pool's type.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity, Func<T> createMethod, bool prewarm = false)
        {
            if (!_GetPool<T>(out var pool))
                pool = _Register(new Pool<T>(capacity, createMethod, prewarm));

            return pool;
        }

        /// <summary>
        /// Clears all pools.
        /// </summary>
        public void ClearAll()
        {
            foreach (var pair in _pools)
            {
                pair.Value.Clear();
            }
        }

        /// <summary>
        /// Disposes all pools.
        /// </summary>
        public void DisposeAll()
        {
            foreach (var pair in _pools)
            {
                pair.Value.OnRemove -= OnRemovePoolHandler;
                pair.Value.Destroy();
            }

            _pools.Clear();
        }

        public bool Has<T>()
        {
            return Has(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(Type type)
        {
            return _pools.ContainsKey(type);
        }

        private void OnRemovePoolHandler(IPoolGeneric sender, Type type)
        {
            _pools.Remove(type);
        }
        
        /// <summary>
        /// Creates a pool with given type.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool _GetPool<T>(out IPool<T> poolResult)
        {
            var isExist = _pools.TryGetValue(typeof(T), out var pool);
            poolResult = isExist ? pool as IPool<T> : null;
            return isExist;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IPool<T> _Register<T>(IPool<T> pool)
        {
            pool.OnRemove += OnRemovePoolHandler;
            _pools[typeof(T)] = pool;
            
            return pool;
        }
    }
}