using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Remove.Components;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class UnloadScenesByNamesSystem : IEcsRunSystem
    {
        EcsFilter<UnloadScenesByNamesEvent> _eventFilter;
        EcsFilter<SceneComponent>.Exclude<SceneUnloadingComponent> _scenesFilter;

        public void Run()
        {
            if (_eventFilter.IsEmpty()) return;

            foreach (var idx in _eventFilter)
            {
                var names = _eventFilter.Get1(idx).SceneNames;
                _UnloadScenesByNames(names);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _UnloadScenesByNames(IList<IAssetName> names)
        {
            foreach (var sceneName in names)
            {
                foreach (var idx in _scenesFilter)
                {
                    var sceneController = _scenesFilter.Get1(idx).Scene;

                    if (!sceneController.SceneName.Equals(sceneName)) continue;

                    _scenesFilter.GetEntity(idx).Get<RemoveEntityComponent>();
                    break;
                }
            }
        }
    }
}