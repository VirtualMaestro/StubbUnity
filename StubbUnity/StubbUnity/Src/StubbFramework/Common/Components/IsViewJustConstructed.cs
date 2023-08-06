using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Common.Components
{
    /**
     * One-frame component-marker shows that the current entity was just created.
     * Always added to the all created IEcsViewLink.
     */
    public struct IsViewJustConstructed : IEcsIgnoreInFilter { }
}