using System;
using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Contexts
{
    public class UnityContext : MonoBehaviour, IStubbContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;
        private IStubbDebug _debugInfo;

        public bool IsDisposed => _world == null;

        public EcsWorld World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _world;
        }

        public virtual void Init(EcsWorld world, IStubbDebug debug = null)
        {
            Stubb.AddContext(this);

            _world = world;
            _debugInfo = debug;

            _rootSystems = InitSystems();

            _debugInfo?.Init(_rootSystems, _world);

            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        protected virtual EcsSystems InitSystems()
        {
            var rootSystems = new EcsSystems(World, "RootSystems");
            rootSystems.AddFeature(new UnityHeadFeature(World));

            var userSystems = InitUserSystems();

            if (userSystems is EcsFeature feature)
                rootSystems.AddFeature(feature);
            else
                rootSystems.Add(userSystems);

            rootSystems.AddFeature(new UnityTailFeature(World));

            return rootSystems;
        }

        protected virtual IEcsSystem InitUserSystems()
        {
            return new EcsSystems(World, "UserSystems");
        }

        /// <summary>
        ///  Global injection for all systems.
        /// </summary>
        protected void Inject(object obj, Type overridenType = null)
        {
            _rootSystems.Inject(obj, overridenType);
        }

        public void Run()
        {
            _rootSystems.Run();
            _debugInfo?.Debug();
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