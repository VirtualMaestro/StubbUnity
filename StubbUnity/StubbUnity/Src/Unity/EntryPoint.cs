using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
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
        private IEcsDebug _debug;

        [Tooltip("Enable UI events emitter to provide ui events to ecs tier")]
        public bool enableUiEmitter;
        public EcsWorld World => _context.World;
        public IEcsDebug Debug => _debug;

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);

            _debug = CreateDebug();
            _context = CreateContext();
            _physicsContext = CreatePhysicsContext();
            
            SetupFeatures(_context);
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (enableUiEmitter)
            {
                var emitter = gameObject.GetComponent<EcsUiEmitter>();
            
                if (emitter != null)
                    _context.MainFeature.InternalSystems.InjectUi(emitter);
            }
            
            _context.Init();
            _physicsContext?.Init();
        }

        /// <summary>
        /// Override if need custom context. 
        /// </summary>
        protected virtual IStubbContext CreateContext()
        {
            return new StubbContext(Debug);
        }

        /// <summary>
        /// Have to be overriden by user for main feature or for all (Head, Main, Tail).
        /// </summary>
        protected virtual void SetupFeatures(IStubbContext context)
        {
            context.HeadFeature = new UnityHeadFeature(World);
            context.TailFeature = new UnityTailFeature(World);
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