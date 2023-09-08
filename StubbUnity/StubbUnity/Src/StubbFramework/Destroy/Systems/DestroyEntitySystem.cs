using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Physics;

namespace StubbUnity.StubbFramework.Destroy.Systems
{
    public sealed class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyEntityAction>.Exclude<DelayComp> _destroyEntityFilter = null;

        public void Run()
        {
            _destroyEntityFilter.Clear();
            
            CollisionManager.EndPhysicsFrame();
        }
    }
}