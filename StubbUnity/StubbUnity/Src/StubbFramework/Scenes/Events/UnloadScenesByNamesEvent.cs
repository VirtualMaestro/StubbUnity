using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component is sent when some set of the scenes should be unloaded.
    /// Component will be removed at the end of the frame.
    /// </summary>
    public struct UnloadScenesByNamesEvent
    {
        public IList<IAssetName> SceneNames;
    }
}