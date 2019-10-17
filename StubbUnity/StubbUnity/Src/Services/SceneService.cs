using System.Collections.Generic;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Components;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using StubbUnity.Extensions;
using StubbUnity.Logging;
using StubbUnity.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        private EcsFilter<SceneComponent, NewSceneMarkerComponent> _newScenesFilter;
        
        public SceneService()
        {
            _newScenesFilter = (EcsFilter<SceneComponent, NewSceneMarkerComponent>) Stubb.World.GetFilter(typeof(EcsFilter<SceneComponent, NewSceneMarkerComponent>));
            SceneManager.sceneLoaded += _SceneLoaded;
        }

        private void _SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _SceneVerification(scene);
            // add new SceneComponent with current loaded scene to the ECS layer and this scene as new
            Stubb.World.NewEntityWith<SceneComponent, NewSceneMarkerComponent>(out var sceneComponent, out var newSceneMarkerComponent);
            sceneComponent.Scene = scene.GetController<SceneController>();
        }
        
        private void _SceneVerification(Scene scene)
        {
            #if DEBUG
                log.Assert(scene.HasController<ISceneController>(), $"SceneVerification: scene '{scene.path}' doesn't contain SceneController!'");
                log.Assert(scene.HasContentController<ISceneContentController>(), $"SceneVerification: scene '{scene.path}' doesn't contain SceneContentController!'");
            #endif
        }
        
        public ISceneLoadingProgress[] Load(in IList<ILoadingSceneConfig> configs)
        {
            SceneLoadingProgress[] progresses = new SceneLoadingProgress[configs.Count];
            int index = 0;

            foreach (var sceneConfig in configs)
            {
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, LoadSceneMode.Additive);
                progresses[index++] = new SceneLoadingProgress(sceneConfig, async);
            }

            return progresses;
        }

        public void Unload(in ISceneController controller)
        {
            log.Assert(!controller.IsDestroyed, $"SceneService.Unload. Scene '{controller.SceneName.FullName}' is already destroyed!");
            controller.HideContent();
            SceneManager.UnloadSceneAsync(controller.SceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        public void LoadingComplete(in ISceneLoadingProgress[] progresses)
        {
            foreach (var progress in progresses)
            {
                LoadingComplete(progress);
            }
        }

        public void LoadingComplete(in ISceneLoadingProgress progress)
        {
            var config = progress.Config;
            
            foreach (var idx in _newScenesFilter)
            {
                var controller = _newScenesFilter.Get1[idx].Scene;
                if (controller.SceneName.Equals(config.Name))
                {
                    if (config.IsActive)
                    {
                        controller.ShowContent();    
                    }

                    if (config.IsMain)
                    {
                        controller.SetAsMain();
                    }
                    
                    break;
                }
            }
        }
    }
}