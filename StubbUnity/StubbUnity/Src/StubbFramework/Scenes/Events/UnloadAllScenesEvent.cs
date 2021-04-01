using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component which is sent when need to unload all scenes.
    /// Component will be removed at the end of the frame.
    /// </summary>
    public struct UnloadAllScenesEvent : IEcsIgnoreInFilter
    {
    }
}