using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.Unity.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Extensions
{
    public static class SceneExtension
    {
        public static bool HasController(this Scene scene) => GetController(scene, out _);

        public static bool GetController(this Scene scene, out ISceneController controller)
        {
            controller = null;
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
            {
                if (!go.TryGetComponent(out controller)) continue;

                return true;
            }

            return false;
        }

        public static bool IsNameEqual(this Scene scene, IAssetName name)
        {
            return scene.path.Equals(name.FullName);
        }
        
        public static bool HasScene(this EcsWorld world, in IAssetName sceneName)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (scene.GetController(out var controller) && controller.SceneName.Equals(sceneName))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Deactivate all game objects on the scene (including SceneController and Content if there are)
        /// </summary>
        public static void Deactivate(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
                go.SetActive(false);
        }

        /// <summary>
        /// Activate all game objects which are on the root (including SceneController and Content if there are). 
        /// </summary>
        public static void Activate(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
                go.SetActive(true);
        }

        /// <summary>
        /// Activate only SceneController game object.
        /// </summary>
        public static void ActivateSceneController(this Scene scene)
        {
            if (scene.GetController(out var controller))
                ((SceneController)controller).gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivate only SceneController game object.
        /// </summary>
        public static void DeactivateSceneController(this Scene scene)
        {
            if (scene.GetController(out var controller))
                ((SceneController)controller).gameObject.SetActive(false);
        }
    }
}