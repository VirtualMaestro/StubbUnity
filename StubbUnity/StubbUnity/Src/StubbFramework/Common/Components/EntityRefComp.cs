using Leopotam.Ecs;
using StubbUnity.StubbFramework.Pooling;

namespace StubbUnity.StubbFramework.Common.Components
{
    public struct EntityRefComp
    {
        private EntityRefData _entityRefData;

        public EntityRefData EntityRefData => _entityRefData;
        
        public void AutoReset(ref EntityRefComp c)
        {
            c._entityRefData ??= Pools.I.Get<EntityRefData>().Get();
            c._entityRefData.Reset();
        }
    }
    
    /// <summary>
    /// Wraps an entity to be shared.
    /// </summary>
    public sealed class EntityRefData
    {
        private EcsEntity _entity;

        public void SetEntity(ref EcsEntity ent)
        {
            _entity = ent;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public void Reset()
        {
            _entity = EcsEntity.Null;
        }

        public void Dispose()
        {
            Reset();
            Pools.I.Get<EntityRefData>().Put(this);
        }
    }
}