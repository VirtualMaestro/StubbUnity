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

        private void OnEnable()
        {
            Create();
        }

        public void Create()
        {
            _world = new EcsWorld();
            _rootSystems = new EcsSystems(_world, "SystemsRoot");
            _userSystems = new EcsSystems(_world, "SystemsBody");

            _rootSystems.Add(SystemsHeadConfig.Create(_world));
            _rootSystems.Add(_userSystems);
            _rootSystems.Add(SystemsTailConfig.Create(_world));
            
            DebugInfo = new UnityEcsDebug();
        }

        public EcsWorld World
        {
            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            get => _world;
        }
        
        public void Add(EcsSystem ecsSystem)
        {
            _userSystems.Add(ecsSystem);
        }

        public void Initialize()
        {
            DebugInfo?.Debug(_rootSystems, _world);

            _rootSystems.Initialize();
        }

        public void Update()
        {
            _rootSystems.Run();
            _world.RemoveOneFrameComponents ();
        }

        public void Dispose()
        {
            _rootSystems.Dispose();
            _world.Dispose();

            _world = null;
            _rootSystems = null;
            _userSystems = null;
        }

        public IStubbDebug DebugInfo { get; set; }
    }
}