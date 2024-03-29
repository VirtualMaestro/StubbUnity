﻿using Leopotam.Ecs;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
    public sealed class ActivateSceneSystem : IEcsRunSystem
    {
        private EcsFilter<ActivateSceneEvent> _activateEventFilter;
        private EcsFilter<SceneComp, SceneInactiveState> _inactiveScenesFilter;

        public void Run()
        {
            if (_activateEventFilter.IsEmpty() || _inactiveScenesFilter.IsEmpty()) return;

            foreach (var idx in _activateEventFilter)
            {
                ref var activateEvent = ref _activateEventFilter.Get1(idx);

                foreach (var idx1 in _inactiveScenesFilter)
                {
                    var sceneController = _inactiveScenesFilter.Get1(idx1).Scene;

                    if (sceneController.SceneName.Equals(activateEvent.SceneName))
                    {
                        sceneController.ShowContent();

                        if (activateEvent.IsMain)
                            sceneController.SetAsMain();
                    }
                }
            }
        }
    }
}