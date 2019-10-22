using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StubbFramework.Scenes;
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
        public IList<ISceneLoadingProgress> Load(in IList<ILoadingSceneConfig> configs)
        {
            IList<ISceneLoadingProgress> progresses = new List<ISceneLoadingProgress>(configs.Count);

            foreach (var sceneConfig in configs)
            {
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, LoadSceneMode.Additive);
                progresses.Add(new SceneLoadingProgress(sceneConfig, async));
            }

            return progresses;
        }

        public void Unload(in ISceneController controller)
        {
            log.Assert(!controller.IsDestroyed, $"SceneService.Unload. Scene '{controller.SceneName.FullName}' is already destroyed!");
            controller.HideContent();
            SceneManager.UnloadSceneAsync(controller.SceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        public IList<ISceneController> LoadingComplete(IList<ISceneLoadingProgress> progresses)
        {
            IList<ISceneController> controllers = new List<ISceneController>(progresses.Count);
            
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                _SceneVerification(scene);

                var controller = scene.GetController<ISceneController>();

                if (controller != null && _MarkProgress(controller, progresses))
                {
                    controllers.Add(controller);
                }

                if (progresses.Count == 0) break;
            }

            return controllers;
        }

        private bool _MarkProgress(ISceneController controller, IList<ISceneLoadingProgress> progresses)
        {
            int index = 0;
            foreach (var progress in progresses)
            {
                if (progress.Config.Name.Equals(controller.SceneName))
                {
                    progresses.RemoveAt(index);
                    return true;
                }

                ++index;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _SceneVerification(Scene scene)
        {
#if DEBUG
            if (!scene.HasController<ISceneController>()) log.Warn($"SceneVerification: scene '{scene.path}' doesn't contain SceneController!'");
            if (!scene.HasContentController<ISceneContentController>()) log.Warn($"SceneVerification: scene '{scene.path}' doesn't contain SceneContentController!'");
#endif
        }
    }
}