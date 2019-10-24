using Leopotam.Ecs;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.View
{
    public class ViewObject : MonoBehaviour, IViewObject
    {
        private EcsEntity _entity = EcsEntity.Null;

        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed => gameObject == null;

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

        void OnDestroy()
        {
            if (IsDisposed) return;

            if (HasEntity) _entity.Destroy();
        }
    }
}