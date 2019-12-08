using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Logging;
using StubbFramework.View;
using StubbFramework.View.Components;
using UnityEngine;

namespace StubbUnity.View
{
    public class ViewObject : MonoBehaviour, IViewObject
    {
        private EcsEntity _entity = EcsEntity.Null;

        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed => gameObject == null;
        public EcsWorld World { get; private set; }

        protected virtual void Awake()
        {
            World = Stubb.GetContext().World;

            _InitEntity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _InitEntity()
        {
            if (!HasEntity)
            {
                _entity = World.NewEntityWith<ViewComponent>(out var viewComponent);
                viewComponent.View = this;
            }
            else
            {
                log.Warn($"{GetType()}._InitEntity: Trying to create an entity while the entity exists in the current ViewObject");
            }
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