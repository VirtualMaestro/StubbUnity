using StubbUnity.StubbFramework;

namespace StubbUnity.Unity
{
    public class UnityTailFeature : SystemTailFeature
    {
        public UnityTailFeature(string name = null) : base(name)
        {
        }

        protected override void SetupSystems()
        {
            base.SetupSystems();
            // InjectGlobal(new SceneService());
        }
    }
}