using System.Collections.Generic;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Common.Components;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Components;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using StubbUnity.Extensions;
using StubbUnity.Logging;
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
            Debug.LogWarning($"SceneService._SceneLoaded. Frame count: {Time.frameCount}, Scene name: {scene.name}, mode: {mode.ToString()}");
            
            // add new SceneComponent with current loaded scene to the ECS layer
            Stubb.World.NewEntityWith<SceneComponent, NewEntityComponent>(out var sceneComponent, out var newEntityComponent);
            sceneComponent.Scene = scene.GetController<SceneController>();
        }
        
        public ISceneLoadingProgress[] Load(in ILoadingScenesConfig config)
        {
            SceneLoadingProgress[] progresses = new SceneLoadingProgress[config.NumScenes];
            int index = 0;
            bool allowSceneActivation = !config.IsActivatingAll;

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
            log.Warn($"SceneService.LoadingComplete. Scenes num: {progresses.Length}");
            foreach (var progress in progresses)
            {
                LoadingComplete(progress);
            }
        }

        public void LoadingComplete(in ISceneLoadingProgress progress)
        {
            
            log.Warn($"SceneService.LoadingComplete. Frame count: {Time.frameCount}, Scene name: {progress.Config.Name}, Progress: {progress.Progress}, IsComplete: {progress.IsComplete}");
        }

//        public void Activate(in ISceneLoadingProgress[] progresses)
//        {
//            foreach (var progress in progresses)
//            {
//                Activate(progress);
//            }
//        }
//
//        public void Activate(in ISceneLoadingProgress progress)
//        {
//             AsyncOperation async = (AsyncOperation) progress.Payload;
//             async.allowSceneActivation = true;
//        }
    }
}