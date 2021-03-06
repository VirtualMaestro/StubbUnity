using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;

namespace StubbUnity.Unity.Features
{
    public class UnityHeadFeature : SystemHeadFeature
    {
        public UnityHeadFeature(EcsWorld world, string name = null) : base(world, name)
        {
        }
    }
}