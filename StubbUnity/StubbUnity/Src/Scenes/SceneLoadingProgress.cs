using StubbFramework.Scenes;
using UnityEngine;

namespace StubbUnity.Scenes
{
    public class SceneLoadingProgress : ISceneLoadingProgress
    {
        public ISceneName SceneName { get; }
        public bool IsComplete => _async.progress >= 0.9;
        public float Progress => _async.progress;
        public object Payload { get; }

        private readonly AsyncOperation _async;

        public SceneLoadingProgress(in ISceneName name, AsyncOperation payload)
        {
            SceneName = name;
            Payload = payload;
            _async = payload;
        }
    }
}