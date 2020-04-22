using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Instrumentation;
using StubbFramework.Common.Names;
using StubbFramework.Logging;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Configurations;
using StubbFramework.Scenes.Services;
using StubbUnity.Extensions;
using StubbUnity.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        public List<ISceneLoadingProgress> Load(in List<ILoadingSceneConfig> configs)
        {
            var progresses = new List<ISceneLoadingProgress>(configs.Count);

            foreach (var sceneConfig in configs)
            {
                // don't allow to load the same scene more than 1 time if multiple is false
                if (!sceneConfig.IsMultiple && HasScene(sceneConfig.Name)) continue;
                
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, LoadSceneMode.Additive);
                progresses.Add(new SceneLoadingProgress(sceneConfig, async));
            }

            return progresses;
        }

        public bool HasScene(in IAssetName sceneName)
        {
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (sceneName.FullName.Equals(scene.path)) return true;
            }

            return false;
        }

        public bool IsSceneReady(in IAssetName sceneName)
        {
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (!sceneName.FullName.Equals(scene.path) || !scene.isLoaded) continue;
                
                _SceneVerification(scene);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unity implementation doesn't require this method.
        /// Use World extension if you want to unload scenes.
        /// </summary>
        /// <param name="controller"></param>
        public void Unload(in ISceneController controller)
        {}

        public ISceneController GetLoadedSceneController(ISceneLoadingProgress progress)
        {
            // start from 1, skip first scene which is root
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                _SceneVerification(scene);

                var controller = scene.GetController();

                // is this scene new loaded
                if (controller == null || controller.HasEntity) continue;
                if (progress.Config.Name.Equals(controller.SceneName)) return controller;
            }

            throw new InstanceNotFoundException($"Scene '{progress.Config.Name}' wasn't found between loaded scenes!");
        }

        [Conditional("DEBUG")]
        private void _SceneVerification(Scene scene)
        {
            if (!scene.HasController())
            {
                log.Error($"SceneVerification: scene '{scene.path}' doesn't contain SceneController!'");
            }

            if (!scene.HasContentController<ISceneContentController>())
            {
                log.Error($"SceneVerification: scene '{scene.path}' doesn't contain SceneContentController!'");
            }
        }
    }
}