using Leopotam.Ecs;
using StubbFramework.Common.Names;
using UnityEngine.SceneManagement;

namespace StubbUnity.Extensions
{
    public static class WorldExtension
    {
        public static bool HasScene(this EcsWorld world, in IAssetName sceneName)
        {
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var controller = scene.GetController();

                if (controller != null && controller.SceneName.Equals(sceneName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}