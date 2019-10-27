using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbFramework;
using UnityEngine;

namespace StubbUnity.Contexts
{
    public class UnityContext : MonoBehaviour, IStubbContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;
        private IStubbDebug _debugInfo;

        public bool IsDisposed => _world == null;

        public EcsWorld World
        {
            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            get => _world;
        }
        
        public void Init()
        {
            Init(new EcsWorld(), null);
        }

        public virtual void Init(EcsWorld world)
        {
            Init(world, null);
        }
        
        public virtual void Init(IStubbDebug debug)
        {
            Init(new EcsWorld(), debug);
        }
        
        public virtual void Init(EcsWorld world, IStubbDebug debug)
        {
            _world = world;
            _debugInfo = debug;

            _rootSystems = InitSystems();
            
            _debugInfo?.Debug(_rootSystems, _world);

            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        protected virtual EcsSystems InitSystems()
        {
            var rootSystems = new EcsSystems(World, "RootSystems");
            rootSystems.Add(new UnitySystemHeadFeature(World));
            rootSystems.Add(InitUserSystems());
            rootSystems.Add(new UnitySystemTailFeature(World));

            return rootSystems;
        }
        
        protected virtual IEcsSystem InitUserSystems()
        {
            return new EcsSystems(World, "UserSystems");
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
        }
    }
}