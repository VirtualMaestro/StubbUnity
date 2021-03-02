using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class UnloadNonNewScenesSystem : IEcsRunSystem
    {
        private EcsFilter<UnloadNonNewScenesEvent> _eventFilter;
        private EcsFilter<SceneComponent>.Exclude<SceneLoadedComponent, SceneUnloadingComponent> _nonNewScenesFilter;

        public void Run()
        {
            if (_eventFilter.IsEmpty()) return;

            _nonNewScenesFilter.MarkRemove();
        }
    }
}