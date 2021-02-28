using Leopotam.Ecs;
using StubbUnity.StubbFramework.Physics.Systems;

namespace StubbUnity.StubbFramework.Physics
{
    public class PhysicsTailFeature : EcsFeature
    {
        public PhysicsTailFeature(EcsWorld world, string name = null, bool isEnable = true) : base(world, name, isEnable)
        {
        }

        protected override void SetupSystems()
        {
            Add(new CleanupCollisionSystem());
        }
    }
}