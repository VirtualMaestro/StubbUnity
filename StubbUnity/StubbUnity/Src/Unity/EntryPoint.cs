using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Core;
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
        private IEcsDebug _debug;

        // TODO: add possibility turn on/off in editor
        public bool injectUi;
        public EcsWorld World => _world;
        public IEcsDebug Debug => _debug;

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);

            _world = CreateWorld();
            _debug = CreateDebug();
            _context = CreateContext();

            _physicsContext = CreatePhysicsContext();

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (injectUi)
            {
                var emitter = gameObject.GetComponent<EcsUiEmitter>();
            
                if (emitter != null)
                    _context.UserFeature.InternalSystems.InjectUi(emitter);
            }
            
            _context.Init();
            _physicsContext?.Init();
        }

        /// <summary>
        /// Override if need custom context. 
        /// </summary>
        protected virtual IStubbContext CreateContext()
        {
            var context = new StubbContext(World, Debug);
            context.HeadFeature = new UnityHeadFeature(World);
            context.TailFeature = new UnityTailFeature(World);
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
        protected virtual IEcsDebug CreateDebug()
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