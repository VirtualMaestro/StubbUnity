using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Extensions;

namespace StubbUnity.Services
{
    public class InitializeServiceSystem : EcsSystem, IEcsPreInitSystem
    {
        public void PreInit()
        {
            World.AddSceneService(new SceneService());
        }
    }
}