using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent for every loaded scene.
    /// </summary>
    public struct SceneLoadingCompleteEvent
    {
        public IAssetName SceneName;
    }
}