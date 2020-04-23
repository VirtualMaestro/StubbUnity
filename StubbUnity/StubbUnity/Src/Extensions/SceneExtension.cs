using JetBrains.Annotations;
using StubbFramework.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Extensions
{
    public static class SceneExtension
    {
        public static bool HasController(this Scene scene) => GetController(scene) != null;

        [CanBeNull]
        public static ISceneController GetController(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
            {
                var controller = go.GetComponent<ISceneController>();

                if (controller == null) continue;

                return controller;
            }

            return null;
        }
    }
}