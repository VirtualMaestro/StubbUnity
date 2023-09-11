using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;

namespace StubbUnity.StubbFramework.Physics
{
    public class PhysicsContext : StubbContext, IPhysicsContext
    {
        public PhysicsContext(EcsWorld world) : base(world)
        {
        }

        protected override void InitFeatures()
        {
            MainFeature = new EcsFeature(World, "UserPhysicsSystems");
            TailFeature = new PhysicsTailFeature(World);
        }

        public override void Destroy()
        {
            RootSystems.Destroy();
            RootSystems = null;
        }
    }
}