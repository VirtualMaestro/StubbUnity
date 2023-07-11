using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Common.Components
{
    /**
     * Component-marker shows that the current entity was just created.
     * Always added to the all created IEcsViewLink.
     */
    public struct IsJustCreatedComponent : IEcsIgnoreInFilter { }
}