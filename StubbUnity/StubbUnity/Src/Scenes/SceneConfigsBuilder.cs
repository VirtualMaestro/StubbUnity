using System.Collections.Generic;
using StubbFramework.Scenes.Configurations;

namespace StubbUnity.Scenes
{
    public class SceneConfigsBuilder
    {
        public static SceneConfigsBuilder Create => new SceneConfigsBuilder();
        
        private readonly List<LoadingSceneConfig> _configs;

        public SceneConfigsBuilder()
        {
            _configs = new List<LoadingSceneConfig>();
        }

        public SceneConfigsBuilder Add(string sceneName, string scenePath = null, bool isActive = true, bool isMain = false, object payload = null)
        {
            var config = new LoadingSceneConfig(new SceneName(sceneName, scenePath)) {IsActive = false, IsMain = isMain, Payload = payload};
            _configs.Add(config);
            return this;
        }
        
        public List<LoadingSceneConfig> Build => _configs;
    }
}