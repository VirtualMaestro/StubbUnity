using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;

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
    }
}