using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Scenes.Components
{
    /// <summary>
    /// One-frame marker-component which determines whether state (active/inactive) of a scene was changed.
    /// </summary>
    public struct SceneChangedStateComponent : IEcsIgnoreInFilter
    {
    }
}