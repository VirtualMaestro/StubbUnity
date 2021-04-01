using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Components
{
    /// <summary>
    /// Contains list of the progresses for loading scenes.
    /// </summary>
    public struct ActiveLoadingScenesComponent
    {
        public List<ISceneLoadingProgress> Progresses;
        public List<IAssetName> UnloadScenes;
        public bool UnloadOthers;
    }
}