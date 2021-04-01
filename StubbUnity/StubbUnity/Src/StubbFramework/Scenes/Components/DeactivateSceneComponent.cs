using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Scenes.Components
{
    /// <summary>
    /// One-frame component which should be attached to the scene controller entity if this scene needs to be deactivated.
    /// For convenience use World.DeactivateScene().
    /// </summary>
    public struct DeactivateSceneComponent : IEcsIgnoreInFilter
    {
    }
}