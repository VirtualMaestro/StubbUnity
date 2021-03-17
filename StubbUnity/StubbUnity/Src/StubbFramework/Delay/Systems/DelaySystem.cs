using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Time.Components;

namespace StubbUnity.StubbFramework.Delay.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class DelaySystem : IEcsRunSystem
    {
        private EcsFilter<DelayComponent> _filterDelay;
        private EcsFilter<TimeComponent> _filterTime;

        public void Run()
        {
            if (_filterDelay.IsEmpty()) return;
            
            ref var time = ref _filterTime.Single();

            foreach (var index in _filterDelay)
            {
                var delay = _filterDelay.Get1(index);
                delay.Frames--;
                delay.Milliseconds -= time.TimeStep;

                if (delay.Frames <= 0 && delay.Milliseconds <= 0)
                {
                    _filterDelay.GetEntity(index).Del<DelayComponent>();
                }
            }
        }
    }
}