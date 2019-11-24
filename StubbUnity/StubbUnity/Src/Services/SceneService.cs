using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
            List<ISceneLoadingProgress> progresses = new List<ISceneLoadingProgress>(configs.Count);

            foreach (var sceneConfig in configs)
            {
                var async = SceneManager.LoadSceneAsync(sceneConfig.Name.FullName, LoadSceneMode.Additive);
                progresses.Add(new SceneLoadingProgress(sceneConfig, async));
            }

            return progresses;
        }

        /// <summary>
        /// Unity implementation doesn't require this method.
        /// Use World extension if you want to unload scenes.
        /// </summary>
        /// <param name="controller"></param>
        public void Unload(in ISceneController controller)
        {}

        public KeyValuePair<ISceneController, ILoadingSceneConfig>[] LoadingComplete(List<ISceneLoadingProgress> progresses)
        {
            KeyValuePair<ISceneController, ILoadingSceneConfig>[] result = new KeyValuePair<ISceneController, ILoadingSceneConfig>[progresses.Count];
            int resultIndex = 0;
            
            // start from 1, skip first scene which is root
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                _SceneVerification(scene);

                var controller = scene.GetController<ISceneController>();

                if (controller != null && controller.HasEntity == false)
                {
                    var res = _MarkProgress(controller, progresses);
                    if (res != null)
                    {
                        result[resultIndex++] = (KeyValuePair<ISceneController, ILoadingSceneConfig>) res;
                    }
                }

                if (progresses.Count == 0) break;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private KeyValuePair<ISceneController, ILoadingSceneConfig>? _MarkProgress(ISceneController controller, List<ISceneLoadingProgress> progresses)
        {
            KeyValuePair<ISceneController, ILoadingSceneConfig>? result = null;
            int index;
            int progressesCount = progresses.Count;
            
            for (index = 0; index < progressesCount; index++)
            {
                var progress = progresses[index];
                if (progress.Config.Name.Equals(controller.SceneName))
                {
                    result = new KeyValuePair<ISceneController, ILoadingSceneConfig>(controller, progress.Config);
                    break;
                }

                ++index;
            }

            if (index < progressesCount)
            {
                progresses.RemoveAt(index);
            }

            return result;
        }
        
        [Conditional("DEBUG")]
        private void _SceneVerification(Scene scene)
        {
            if (!scene.HasController<ISceneController>())
            {
                log.Warn($"SceneVerification: scene '{scene.path}' doesn't contain SceneController!'");
            }

            if (!scene.HasContentController<ISceneContentController>())
            {
                log.Warn($"SceneVerification: scene '{scene.path}' doesn't contain SceneContentController!'");
            }
        }
    }
}