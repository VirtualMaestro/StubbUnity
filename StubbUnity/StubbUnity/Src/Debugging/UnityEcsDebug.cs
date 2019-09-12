using Leopotam.Ecs;
using StubbFramework;

namespace StubbUnity.Debugging
{
    public class UnityEcsDebug : IStubbDebug
    {
        public void Debug(EcsSystems rootSystems, EcsWorld world)
        {
        #if UNITY_EDITOR
            UnityIntegration.EcsWorldObserver.Create (world);
            UnityIntegration.EcsSystemsObserver.Create (rootSystems);
        #endif
        }
    }
}