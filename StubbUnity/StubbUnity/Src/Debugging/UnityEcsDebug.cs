using Leopotam.Ecs;
using StubbFramework;

#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif

namespace StubbUnity.Debugging
{
    public class UnityEcsDebug : IStubbDebug
    {
        public void Debug(EcsSystems rootSystems, EcsWorld world)
        {
        #if UNITY_EDITOR
            EcsWorldObserver.Create (world);
            EcsSystemsObserver.Create (rootSystems);
        #endif
        }
    }
}