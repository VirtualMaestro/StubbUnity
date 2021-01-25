using Leopotam.Ecs;
using StubbUnity.StubbFramework;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.Unity.Contexts;
using StubbUnity.Unity.Debugging;
using StubbUnity.Unity.Logging;
using UnityEngine;

namespace StubbUnity.Unity
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] 
        private UnityContext context;
        
        [SerializeField] 
        private UnityPhysicsContext physicsContext;
        
        private IStubbContext _context;
        private IPhysicsContext _physicsContext;

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);
            _context = context == null ? GetComponent<IStubbContext>() : context;
            log.Assert(_context != null,
                "Context is missing! Attach UnityContext to the GameObject where EntryPoint script is attached!");
            _context?.Init(new EcsWorld(), new UnityEcsDebug());
            
            _physicsContext = physicsContext == null ? GetComponent<IPhysicsContext>() : physicsContext;
            _physicsContext?.Init(_context.World);
            
            DontDestroyOnLoad(gameObject);
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