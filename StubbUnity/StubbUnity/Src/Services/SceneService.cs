using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// This service mostly for internal use.
    /// It is action point which should be implemented for specific engine.
    /// </summary>
    public class SceneService : ISceneService
    {
        /// <summary>
        /// Use World.LoadScene(s) if a scene needs to be loaded.
        /// </summary>
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
        /// Use World.UnloadScene(s) if a scene needs to be unloaded.
        /// </summary>
        public void Unload(in ISceneController controller)
        {
            var scene = ((SceneController) controller).Scene;
            SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

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

            throw new Exception($"Scene '{progress.Config.Name}' wasn't found between loaded scenes!");
        }

        [Conditional("DEBUG")]
        private void _SceneVerification(Scene scene)
        {
            if (!scene.HasController())
            {
                log.Error($"SceneVerification: scene '{scene.path}' doesn't contain SceneController!'");
            }
        }
    }
}