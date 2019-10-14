using JetBrains.Annotations;
using StubbFramework.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Extensions
{
    public static class SceneExtension
    {
        [CanBeNull]
        public static ISceneController GetController<T>(this Scene scene) where T : ISceneController
        {
            var gObjects = scene.GetRootGameObjects();
            
            foreach (var gObj in gObjects)
            {
                ISceneController controller = gObj.GetComponent<ISceneController>();
                
                if (controller == null) continue;

                return controller;
            }
            
            return null;
        }

        [CanBeNull]
        public static ISceneContentController GetContentController<T>(this Scene scene) where T : ISceneContentController
        {
            var gObjects = scene.GetRootGameObjects();
            
            foreach (var gObj in gObjects)
            {
                ISceneContentController contentController = gObj.GetComponent<ISceneContentController>();
                
                if (contentController == null) continue;

                return contentController;
            }
            
            return null;
        }
    }
}