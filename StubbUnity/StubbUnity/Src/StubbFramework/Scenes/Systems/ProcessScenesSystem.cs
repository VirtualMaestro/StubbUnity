using Leopotam.Ecs;
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
                ref var loadScenesConfig = ref _loadScenesFilter.Get1(idx);

                _sceneService.Process(loadScenesConfig.Configuration);
            }
        }
    }
}