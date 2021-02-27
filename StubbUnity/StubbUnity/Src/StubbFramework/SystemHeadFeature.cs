using StubbUnity.StubbFramework.Delay.Systems;
using StubbUnity.StubbFramework.Time.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemHeadFeature : EcsFeature
    {
        public SystemHeadFeature(string name = "HeadSystems") : base(name)
        {
        }

        protected override void SetupSystems()
        {
            Add(new TimeSystem());
            Add(new DelaySystem());
        }
    }
}