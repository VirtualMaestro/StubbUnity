using Leopotam.Ecs;
using StubbFramework;
using StubbUnity.Services;

namespace StubbUnity
{
    public class UnitySystemHeadFeature : SystemHeadFeature
    {
        public UnitySystemHeadFeature(EcsWorld world, string name = null) : base(world, name)
        {}
        
        protected override void SetupSystems()
        {
            base.SetupSystems();
            Add(new InitializeServiceSystem());
        }

    }
}