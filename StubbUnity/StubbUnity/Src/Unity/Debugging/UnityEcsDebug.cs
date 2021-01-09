using Leopotam.Ecs;
using StubbFramework.Debugging;

namespace StubbUnity.Debugging
{
    public class UnityEcsDebug : StubbDebug
    {
        public override void Init(EcsSystems rootSystems, EcsWorld world)
        {
            base.Init(rootSystems, world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (rootSystems);
#endif
        }
    }
}