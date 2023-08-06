using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Components;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;
using StubbUnity.StubbFramework.Pooling;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class EcsEntityExtension
    {
        public static void DestroyEntityEndFrame(ref this EcsEntity entity)
        {
            entity.Get<DestroyEntityAction>();
        }

        public static void DestroyEntityWithDelay(ref this EcsEntity entity, long milliseconds)
        {
            entity.Get<DestroyEntityAction>();
            entity.Get<DelayComp>().Milliseconds = milliseconds;
        }
        
        
        /// <summary>
        /// Packs this entity to a EntityRefData instance, so it can be use in a component.
        /// </summary>
        public static EntityRefData GetEntityRefData(ref this EcsEntity entity)
        {
            var entityRefData = Pools.I.Get<EntityRefData>().Get();
            entityRefData.SetEntity(ref entity);
            return entityRefData;
        }

        /// <summary>
        ///  Creates a component EntityRefComp for this entity that contains another given entity
        /// </summary>
        public static ref EntityRefComp GetRef(ref this EcsEntity entity, ref EcsEntity refEntity)
        {
            ref var entityRefComp = ref entity.Get<EntityRefComp>();
            entityRefComp.EntityRefData.SetEntity(ref refEntity);

            return ref entityRefComp;
        }
    }
}