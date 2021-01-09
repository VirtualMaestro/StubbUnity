using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.View;
using StubbFramework.View.Components;
using StubbUnity.Physics.Settings;
using UnityEngine;

namespace StubbUnity.View
{
    public class EcsViewLink : MonoBehaviour, IEcsViewLink
    {
        [SerializeField] private CollisionDispatchProperties triggerProperties;
        [SerializeField] private CollisionDispatchProperties collisionProperties;

        private EcsEntity _entity = EcsEntity.Null;
        private CollisionDispatchSettings _collisionDispatchSettings;

        /// <summary>
        /// int number which represents type for an object.
        /// This type will be used for determination which object it is and for setting up collision pair.
        /// It determines if collision event will be sent during a collision of two objects.
        /// Default value 0, which means no collision events will be sent.
        /// </summary>
        public int TypeId { get; set; }
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed { get; private set; }
        public EcsWorld World { get; private set; }

        private void Start()
        {
            IsDisposed = false;
            World = Stubb.World;
            _collisionDispatchSettings = new CollisionDispatchSettings(this);
            _InitEntity();

            Initialize();
        }

        /// <summary>
        /// Init user's code here.
        /// </summary>
        public virtual void Initialize()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _InitEntity()
        {
            _entity = World.NewEntity();
            _entity.Get<EcsViewLinkComponent>().Value = this;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        public CollisionDispatchProperties GetTriggerProperties()
        {
            return triggerProperties;
        }

        public CollisionDispatchProperties GetCollisionProperties()
        {
            return collisionProperties;
        }

        /// <summary>
        /// Dispose entity and GameObject.
        /// Here should be user's custom dispose logic.
        /// IMPORTANT: in inherited classes should be base.Dispose() invoked. 
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            _collisionDispatchSettings.Dispose();
            _collisionDispatchSettings = null;

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