using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.StubbFramework.Physics.Systems;
using UnityEngine;

namespace StubbUnity.Unity.Contexts
{
    public class UnityPhysicsContext : MonoBehaviour, IPhysicsContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;

        public bool IsDisposed => _world == null;
       
        public EcsWorld World
        {
            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            get => _world;
        }

        public void Init(EcsWorld world, IStubbDebug debug = null)
        {
            Stubb.AddContext(this);

            _world = world;
            _rootSystems = InitSystems();
            _rootSystems.Add(_InitInternalSystems(world));
            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        protected virtual EcsSystems InitSystems()
        {
            return new EcsSystems(World, "Default UnityPhysicsContext systems implementation");
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EcsSystems _InitInternalSystems(EcsWorld world)
        {
            var systems = new EcsSystems(world, "InternalPhysicsSystems");
            systems.Add(new CleanupCollisionSystem());
            return systems;
        }
    }
}