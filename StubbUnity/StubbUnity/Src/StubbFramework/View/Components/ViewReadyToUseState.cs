using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.View.Components
{
    /**
     * Component-marker that shows that state of the view is ready to use.
     * Always added to the all created IEcsViewLink.
     */
    public struct ViewReadyToUseState : IEcsIgnoreInFilter { }
}