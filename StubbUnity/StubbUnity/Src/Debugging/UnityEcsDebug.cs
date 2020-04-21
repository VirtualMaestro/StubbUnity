using Leopotam.Ecs;
using StubbFramework;

namespace StubbUnity.Debugging
{
    public class UnityEcsDebug : IStubbDebug
    {
        public void Debug(EcsSystems rootSystems, EcsWorld world)
        {
        #if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (rootSystems);
        #endif
        }
    }
}