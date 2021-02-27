using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Systems;
using StubbUnity.StubbFramework.Time.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemHeadFeature : EcsFeature
    {
        public SystemHeadFeature(EcsWorld world, string name = null) : base(world, name ?? "HeadSystems")
        {
        }

        protected override void SetupSystems()
        {
            Add(new TimeSystem());
            Add(new DelaySystem());
        }
    }
}