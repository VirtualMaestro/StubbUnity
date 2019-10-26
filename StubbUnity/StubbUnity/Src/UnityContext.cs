using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbFramework;
using StubbUnity.Debugging;
using UnityEngine;

namespace StubbUnity
{
    public class UnityContext : MonoBehaviour, IStubbContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;
        private EcsSystems _userSystems;

        public bool IsDisposed => _world == null;

        public virtual void Create()
        {
            _world = new EcsWorld();
            _rootSystems = new EcsSystems(_world, "RootSystems");
            _userSystems = new EcsSystems(_world, "UserSystems");

            _rootSystems.Add(new UnitySystemHeadFeature());
            _rootSystems.Add(_userSystems);
            _rootSystems.Add(new UnitySystemTailFeature());
            
            DebugInfo = new UnityEcsDebug();
        }

        public EcsWorld World
        {
            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            get => _world;
        }
        
        public void Add(IEcsSystem ecsSystem)
        {
            _userSystems.Add(ecsSystem);
        }

        public void Initialize()
        {
            DebugInfo?.Debug(_rootSystems, _world);

            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        public void Run()
        {
            _rootSystems.Run();
            _world.EndFrame();
        }

        public void Dispose()
        {
            _rootSystems.Destroy();
            _world.Destroy();

            _world = null;
            _rootSystems = null;
            _userSystems = null;
        }

        public IStubbDebug DebugInfo { get; set; }
    }
}