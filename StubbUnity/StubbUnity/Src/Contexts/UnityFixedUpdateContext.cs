using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using UnityEngine;

namespace StubbUnity.Contexts
{
    public class UnityFixedUpdateContext : MonoBehaviour, IFixedUpdateContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;

        public bool IsDisposed => _world == null;
       
        public EcsWorld World
        {
            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            get => _world;
        }

        public void Init(EcsWorld world)
        {
            _world = world;

            _rootSystems = InitSystems();
            
            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        protected virtual EcsSystems InitSystems()
        {
            return new EcsSystems(World, "Default UnityFixedUpdateContext systems implementation");
        }

        public void Run()
        {
            _rootSystems.Run();
        }

        public void Dispose()
        {
            _rootSystems.Destroy();
            _rootSystems = null;
            _world = null;
        }
    }
}