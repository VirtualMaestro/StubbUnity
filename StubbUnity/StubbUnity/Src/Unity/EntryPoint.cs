using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.Unity.Debugging;
using StubbUnity.Unity.Logging;
using UnityEngine;

namespace StubbUnity.Unity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private IPhysicsContext _physicsContext;
        private EcsWorld _world;

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);

            _world = CreateWorld();
            _context = CreateContext();
            _context.Init(_world, CreateDebug());

            _physicsContext = CreatePhysicsContext();
            _physicsContext?.Init(_world);

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Override if need custom context. 
        /// </summary>
        protected virtual IStubbContext CreateContext()
        {
            var context = new StubbContext();
            context.HeadSystemFeature = new UnityHeadFeature();
            context.TailSystemFeature = new UnityTailFeature();
            return context;
        }

        /// <summary>
        /// Override if need custom World. 
        /// </summary>
        protected virtual EcsWorld CreateWorld()
        {
            return new EcsWorld(CreateWorldConfig());
        }

        /// <summary>
        /// Override if need custom WorldConfig. 
        /// </summary>
        protected virtual EcsWorldConfig CreateWorldConfig()
        {
            return default;
        }

        /// <summary>
        /// Override if need custom Debug. 
        /// </summary>
        protected virtual IStubbDebug CreateDebug()
        {
            return new UnityEcsDebug();
        }

        /// <summary>
        /// Override if need physics context. 
        /// </summary>
        protected virtual IPhysicsContext CreatePhysicsContext()
        {
            return default;
        }

        private void Update()
        {
            _context.Run();
        }

        private void FixedUpdate()
        {
            _physicsContext?.Run();
        }

        private void OnDestroy()
        {
            _context.Dispose();
            _physicsContext?.Dispose();
        }
    }
}