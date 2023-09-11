using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Core.Events;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.StubbFramework.Time;
using StubbUnity.Unity.Debugging;
using StubbUnity.Unity.Logging;
using StubbUnity.Unity.Scenes;
using UnityEngine;

namespace StubbUnity.Unity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private IPhysicsContext _physicsContext;
        private bool _hasFocus = true;
        private bool _isPaused;

        [SerializeField]
        private bool enablePhysics;
        
        [Tooltip("Enable UI events emitter to provide ui events to ecs tier")]
        [SerializeField]
        private bool enableUiEmitter;
        public EcsWorld World => _context.World;
        public IEcsDebug Debug { get; private set; }

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);

            Debug = CreateDebug();
            _context = CreateContext();
            
            if (enablePhysics)
                _physicsContext = CreatePhysicsContext();

            MapServices(_context);
            
            OnCreateSystems(_context.MainFeature);
            
            if (_physicsContext != null)
                OnCreatePhysicsSystems(_physicsContext.MainFeature);

            if (enableUiEmitter) 
                _context.MainFeature.InternalSystems.InjectUi(gameObject.AddComponent<EcsUiEmitter>());
        }

        private void Start()
        {
            OnPreInitialize();
            
            _context.Init();
            _physicsContext?.Init();
            
            DontDestroyOnLoad(gameObject);
            
            OnInitialize();
        }

        /// <summary>
        /// Override for injecting, creating and adding user systems to main loop (Update).
        /// Awake phase.
        /// </summary>
        protected virtual void OnCreateSystems(EcsFeature feature)
        { }

        /// <summary>
        /// Override for injecting, creating and adding user systems to physics loop (FixedUpdate).
        /// Awake phase.
        /// </summary>
        protected virtual void OnCreatePhysicsSystems(EcsFeature feature)
        { }

        /// <summary>
        /// Invokes in Start phase before contexts are initialized. 
        /// </summary>
        protected virtual void OnPreInitialize()
        { }
        
        /// <summary>
        /// Invokes in Start phase after contexts are initialized. 
        /// </summary>
        protected virtual void OnInitialize()
        { }
        
        /// <summary>
        /// Override if need custom context. 
        /// </summary>
        protected virtual IStubbContext CreateContext()
        {
            return new StubbContext(Debug);
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
            return new PhysicsContext(_context.World);
        }
        
        /// <summary>
        /// Maps Unity specific services.
        /// </summary>
        protected void MapServices(IStubbContext context)
        {
            context.Inject(new SceneService(context.World));
            context.Inject(new TimeService());
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
            _context.Destroy();
            _physicsContext?.Destroy();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == _hasFocus) return;

            _hasFocus = hasFocus;
            if (_hasFocus)
                World.NewEntity().Get<ApplicationFocusOnEvent>();
            else
                World.NewEntity().Get<ApplicationFocusOffEvent>();
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused == _isPaused) return;

            _isPaused = isPaused;
            if (_isPaused)
                World.NewEntity().Get<ApplicationPauseOnEvent>();
            else
                World.NewEntity().Get<ApplicationPauseOffEvent>();
        }

        private void OnApplicationQuit()
        {
            World.NewEntity().Get<ApplicationQuitEvent>();
        }
    }
}