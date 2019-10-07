using StubbFramework.Scenes.Configurations;
using StubbFramework.Services;
using UnityEngine.SceneManagement;

namespace StubbUnity.Services
{
    public class SceneService : ISceneService
    {
        public void Load(ILoadingSceneConfig config)
        {
            SceneManager.LoadSceneAsync(config.SceneName, config.IsAdditive ? LoadSceneMode.Additive: LoadSceneMode.Single);
        }

        public void Unload(string sceneName)
        {
            
        }
    }
}