using System.Collections.Generic;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using StubbUnity.Logging;
using StubbUnity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        public SceneService()
        {
            SceneManager.sceneLoaded += _SceneLoaded;
        }

        private void _SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.LogWarning($"SceneService._SceneLoaded. Scene name: {scene.name}, mode: {mode.ToString()}");
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

        public void Unload(in ISceneName sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        public void Unload(in IList<ISceneName> sceneNames)
        {
            foreach (var name in sceneNames)
            {
                Unload(name);
            }
           
            Resources.UnloadUnusedAssets();
        }

        public void LoadingComplete(in ISceneLoadingProgress[] progresses)
        {
            log.Warn("SceneService.LoadingComplete: progresses num " + progresses.Length);
            foreach (var progress in progresses)
            {
                LoadingComplete(progress);
            }
        }

        public void LoadingComplete(in ISceneLoadingProgress progress)
        {
            log.Warn($"Scene name: {progress.Config.Name}, Progress: {progress.Progress}, IsComplete: {progress.IsComplete}");
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