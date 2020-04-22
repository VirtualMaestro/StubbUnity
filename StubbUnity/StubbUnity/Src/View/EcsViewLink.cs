using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.View;
using StubbFramework.View.Components;
using UnityEngine;

namespace StubbUnity.View
{
    public class EcsViewLink : MonoBehaviour, IEcsViewLink
    {
        private EcsEntity _entity = EcsEntity.Null;
        private bool _isDisposed;
        
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed => _isDisposed;
        public EcsWorld World { get; private set; }

        private void Awake()
        {
            _isDisposed = false;
            World = Stubb.World;

            _InitEntity();
            Ready();
        }

        /// <summary>
        /// Init user's here.
        /// </summary>
        protected virtual void Ready()
        {
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _InitEntity()
        {
            _entity = World.NewEntity();
            _entity.Set<EcsViewLinkComponent>().Value = this;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        /// <summary>
        /// Dispose entity and GameObject.
        /// Here should be user's custom dispose logic.
        /// IMPORTANT: in inherited classes should be base.Dispose() invoked. 
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed) return;
            _isDisposed = true;

            if (HasEntity) _entity.Destroy();
            if (gameObject != null) Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (IsDisposed) return;

            Dispose();
        }
    }
}