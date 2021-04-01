using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component is sent when need to load one or bunch scenes. Will be removed at the end of the loop
    /// For convenience use World.LoadScenes().
    /// </summary>
    public struct LoadScenesEvent
    {
        /// <summary>
        /// List of the scenes configurations.
        /// </summary>
        public List<ILoadingSceneConfig> LoadingScenes;
        /// <summary>
        /// List of the names for the scenes which have to be unloaded when scenes from the LoadingScenes have been loaded.
        /// </summary>
        public List<IAssetName> UnloadingScenes;
        /// <summary>
        /// Param show whether others scenes will be unloaded when LoadingScenes have been loaded.
        /// </summary>
        public bool UnloadOthers;
    }
}