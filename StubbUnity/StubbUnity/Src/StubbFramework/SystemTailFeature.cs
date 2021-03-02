using Leopotam.Ecs;
using StubbUnity.StubbFramework.Remove.Systems;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.View.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemTailFeature : EcsFeature
    {
        public SystemTailFeature(EcsWorld world, string name = "TailSystems") : base(world, name)
        {
            Add(new SceneFeature(World));
            Add(new RemoveEcsViewLinkSystem());
            Add(new RemoveEntitySystem());
        }
    }
}