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
        public int worldId;
        private EcsEntity _entity = EcsEntity.Null;

        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed => gameObject == null;
        public EcsWorld World { get; private set; }

        private void Awake()
        {
            World = Stubb.GetContext(worldId).World;

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

        public void Dispose()
        {
            if (IsDisposed == false)
            {
                Destroy(gameObject);                    
            }
        }

        protected virtual void OnDestroy()
        {
            if (IsDisposed) return;

            if (HasEntity) _entity.Destroy();
        }
    }
}