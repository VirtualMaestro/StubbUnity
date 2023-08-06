using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Physics.Components;

namespace StubbUnity.StubbFramework.Physics.Systems
{
    public sealed class CleanupCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<CollisionComp> _cleanupCollisionFilter;
        
        public void Run()
        {
            if (_cleanupCollisionFilter.IsEmpty())
                return;
            
            _cleanupCollisionFilter.Clear();
        }
    }
}