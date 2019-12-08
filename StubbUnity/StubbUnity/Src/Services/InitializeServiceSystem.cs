using Leopotam.Ecs;
using StubbFramework.Extensions;

namespace StubbUnity.Services
{
    public class InitializeServiceSystem : IEcsPreInitSystem
    {
        private EcsWorld World;
        
        public void PreInit()
        {
            World.AddSceneService(new SceneService());
        }
    }
}