using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.View.Components
{
    /**
     * One-frame component-marker shows that the current entity was just initialized and ready to use.
     * Always added to the all created IEcsViewLink.
     */
    public struct IsViewJustInitialized : IEcsIgnoreInFilter { }
}