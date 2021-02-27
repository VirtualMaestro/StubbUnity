using StubbUnity.StubbFramework.Remove.Systems;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.View.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemTailFeature : EcsFeature
    {
        public SystemTailFeature(string name = "TailSystems") : base(name)
        {
        }

        protected override void SetupSystems()
        {
            Add(new SceneFeature());
            Add(new RemoveEcsViewLinkSystem());
            Add(new RemoveEntitySystem());
        }
    }
}