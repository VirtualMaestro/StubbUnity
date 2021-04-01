using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component will be sent when all scenes which are not marked with IsNewComponent need to be unloaded.
    /// So, all non-new scenes will be unloaded.
    /// Component will be removed at the end of the frame.
    /// </summary>
    public struct UnloadNonNewScenesEvent : IEcsIgnoreInFilter
    {}
}