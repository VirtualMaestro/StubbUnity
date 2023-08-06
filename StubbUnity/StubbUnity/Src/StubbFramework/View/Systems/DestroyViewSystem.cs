using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;
using StubbUnity.StubbFramework.View.Components;

namespace StubbUnity.StubbFramework.View.Systems
{
    public sealed class DestroyViewSystem : IEcsRunSystem
    {
        private EcsFilter<ViewComp, DestroyEntityAction>.Exclude<DelayComp> _destroyViewFilter;
            
        public void Run()
        {
            if (_destroyViewFilter.IsEmpty()) 
                return;
            
            foreach (var idx in _destroyViewFilter)
            {
                var view = _destroyViewFilter.Get1(idx).View;
                view.Destroy();
            }
        }
    }
}