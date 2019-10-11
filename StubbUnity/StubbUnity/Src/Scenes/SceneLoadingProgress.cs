using StubbFramework.Scenes;
using UnityEngine;

namespace StubbUnity.Scenes
{
    public class SceneLoadingProgress : ISceneLoadingProgress
    {
        public ISceneName SceneName { get; }
        public bool IsComplete => _async.progress >= 0.9f;
        public float Progress => _async.progress;
        public object Payload { get; }

        private readonly AsyncOperation _async;

        public SceneLoadingProgress(in ISceneName name, in AsyncOperation payload)
        {
            SceneName = name;
            Payload = payload;
            _async = payload;
        }
    }
}