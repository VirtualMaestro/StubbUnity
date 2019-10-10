using System.Collections.Generic;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using StubbUnity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        public ISceneLoadingProgress[] Load(ILoadingScenesConfig config)
        {
            SceneLoadingProgress[] progresses = new SceneLoadingProgress[config.NumScenes];
            int index = 0;
            bool allowSceneActivation = !config.IsActivatingAll;

            foreach (var sceneConfig in config)
            {
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, sceneConfig.IsAdditive ? LoadSceneMode.Additive: LoadSceneMode.Single);
                async.allowSceneActivation = allowSceneActivation;
                progresses[index++] = new SceneLoadingProgress(sceneConfig.Name, async);
            }

            return progresses;
        }

        public void Unload(ISceneName sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        public void Unload(IList<ISceneName> sceneNames)
        {
            foreach (var name in sceneNames)
            {
                Unload(name);
            }
           
            Resources.UnloadUnusedAssets();
        }

        public void Activate(ISceneLoadingProgress[] progresses)
        {
            foreach (var progress in progresses)
            {
                Activate(progress);
            }
        }

        public void Activate(ISceneLoadingProgress progress)
        {
             AsyncOperation async = (AsyncOperation) progress.Payload;
             async.allowSceneActivation = true;
        }
    }
}