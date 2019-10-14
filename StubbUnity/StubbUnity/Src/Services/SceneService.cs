using System.Collections.Generic;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Common.Components;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Components;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using StubbUnity.Extensions;
using StubbUnity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        private EcsFilter<SceneComponent, NewEntityComponent> _newScenesFilter;
        
        public SceneService()
        {
            _newScenesFilter = (EcsFilter<SceneComponent, NewEntityComponent>) Stubb.World.GetFilter(typeof(EcsFilter<SceneComponent, NewEntityComponent>));
            SceneManager.sceneLoaded += _SceneLoaded;
        }

        private void _SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // add new SceneComponent with current loaded scene to the ECS layer
            Stubb.World.NewEntityWith<SceneComponent, NewEntityComponent>(out var sceneComponent, out var newEntityComponent);
            sceneComponent.Scene = scene.GetController<SceneController>();
        }
        
        public ISceneLoadingProgress[] Load(in ILoadingScenesConfig config)
        {
            SceneLoadingProgress[] progresses = new SceneLoadingProgress[config.NumScenes];
            int index = 0;

            foreach (var sceneConfig in config)
            {
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, sceneConfig.IsSingle ? LoadSceneMode.Single: LoadSceneMode.Additive);
                progresses[index++] = new SceneLoadingProgress(sceneConfig, async);
            }

            return progresses;
        }

        public void Unload(in IList<ISceneName> sceneNames)
        {
            foreach (var name in sceneNames)
            {
                Unload(name);
            }
           
            Resources.UnloadUnusedAssets();
        }

        public void Unload(in ISceneName sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
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
            var sceneName = config.Name.FullName;
            
            foreach (var idx in _newScenesFilter)
            {
                var controller = _newScenesFilter.Get1[idx].Scene;
                if (controller.SceneName.FullName == sceneName)
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