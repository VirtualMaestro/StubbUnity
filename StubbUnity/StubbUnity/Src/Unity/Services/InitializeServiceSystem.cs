using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace StubbUnity.Unity.Services
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InitializeServiceSystem : IEcsPreInitSystem
    {
        private EcsWorld World;
        
        public void PreInit()
        {
            World.AddSceneService(new SceneService());
        }
    }
}