using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.Unity.Services;

namespace StubbUnity.Unity
{
    public class UnityTailFeature : SystemTailFeature, IEcsInitSystem
    {
        public UnityTailFeature(EcsWorld world, string name = null) : base(world, name)
        {
        }

        public void Init()
        {
            World.NewEntity().Get<SceneServiceComponent>().SceneService = new SceneService();
        }
    }
}