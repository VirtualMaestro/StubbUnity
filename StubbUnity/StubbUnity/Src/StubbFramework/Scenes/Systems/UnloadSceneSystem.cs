using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Remove.Components;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Services;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class UnloadSceneSystem : IEcsRunSystem
    {
        private EcsFilter<SceneComponent, SceneUnloadingComponent> _unloadingScenesFilter;
        private EcsFilter<SceneComponent, RemoveEntityComponent>.Exclude<SceneUnloadingComponent> _unloadScenesFilter;
        private EcsWorld _world;
        private ISceneService _sceneService;

        public void Run()
        {
            _MarkRemoved();
            _MarkUnloaded();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _MarkRemoved()
        {
            if (_unloadingScenesFilter.IsEmpty()) return;

            foreach (var idx in _unloadingScenesFilter)
            {
                _unloadingScenesFilter.GetEntity(idx).Get<RemoveEntityComponent>();

                var controller = _unloadingScenesFilter.Get1(idx).Scene;
                _sceneService.Unload(controller);
                controller.Dispose();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _MarkUnloaded()
        {
            if (_unloadScenesFilter.IsEmpty()) return;

            foreach (var idx in _unloadScenesFilter)
            {
                ref var entity = ref _unloadScenesFilter.GetEntity(idx);
                entity.Del<RemoveEntityComponent>();
                entity.Get<SceneUnloadingComponent>();
                entity.Get<DeactivateSceneComponent>();
            }
        }
    }
}