using Leopotam.Ecs;
using StubbFramework.Common.Names;
using StubbFramework.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Extensions
{
    public static class WorldExtension
    {
        public static bool HasScenes(this EcsWorld world, in IAssetName sceneName)
        {
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var controller = scene.GetController<ISceneController>();
                
                if (controller != null && controller.SceneName.Equals(sceneName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}