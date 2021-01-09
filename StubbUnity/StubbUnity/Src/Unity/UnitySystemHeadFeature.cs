using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.Unity.Services;

namespace StubbUnity.Unity
{
    public class UnitySystemHeadFeature : SystemHeadFeature
    {
        public UnitySystemHeadFeature(EcsWorld world, string name = null) : base(world, name)
        {
        }

        protected override void SetupSystems()
        {
            base.SetupSystems();
            Add(new InitializeServiceSystem());
        }
    }
}