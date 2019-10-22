using System.Collections.Generic;
using StubbFramework.Scenes.Configurations;

namespace StubbUnity.Scenes
{
    public class SceneConfigsBuilder
    {
        public static SceneConfigsBuilder Create => new SceneConfigsBuilder();
        
        private readonly IList<ILoadingSceneConfig> _configs;

        public SceneConfigsBuilder()
        {
            _configs = new List<ILoadingSceneConfig>();
        }

        public SceneConfigsBuilder Add(string sceneName, string scenePath = null, bool isActive = true, bool isMain = false, object payload = null)
        {
            var config = new LoadingSceneConfig(new SceneName(sceneName, scenePath), isActive, isMain, payload);
            _configs.Add(config);
            return this;
        }
        
        public IList<ILoadingSceneConfig> Build => _configs;
    }
}