using Leopotam.Ecs;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Services;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
    public sealed class ProcessScenesSystem : IEcsRunSystem
    {
        private EcsFilter<ProcessScenesEvent> _loadScenesFilter;
        private ISceneService _sceneService;

        public void Run()
        {
            if (_loadScenesFilter.IsEmpty()) return;

            foreach (var idx in _loadScenesFilter)
            {
                ref var loadScenes = ref _loadScenesFilter.Get1(idx);

                _sceneService.Process(_CreateProcessSetSceneConfig(loadScenes));
            }
        }

        private ProcessSetScenesConfig _CreateProcessSetSceneConfig(ProcessScenesEvent processSceneEvent)
        {
            var loadingSceneSet = ProcessSetScenesConfig.Get();
            loadingSceneSet.Name = processSceneEvent.Name;
            loadingSceneSet.UnloadOthers = processSceneEvent.UnloadOthers;
            loadingSceneSet.AddToLoad(processSceneEvent.LoadingScenes);
            loadingSceneSet.AddToUnload(processSceneEvent.UnloadingScenes);

            return loadingSceneSet;
        }
    }
}