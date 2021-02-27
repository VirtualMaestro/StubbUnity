using Leopotam.Ecs;
using StubbUnity.StubbFramework.Remove.Systems;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.View.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemTailFeature : EcsFeature
    {
        public SystemTailFeature(EcsWorld world, string name = null) : base(world, name ?? "TailSystems")
        {
        }

        protected override void SetupSystems()
        {
            Add(new SceneFeature(World));
            Add(new RemoveEcsViewLinkSystem());
            Add(new RemoveEntitySystem());
        }
    }
}