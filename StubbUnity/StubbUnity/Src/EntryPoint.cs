using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Logging;
using StubbFramework.Physics;
using StubbUnity.Debugging;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private IPhysicsContext _physicsContext;
        
        private void Start()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);
            _context = GetComponent<IStubbContext>();
            log.Assert(_context != null, "Context is missing! Attach UnityContext to the GameObject where EntryPoint script is attached!");
            _context?.Init(new EcsWorld(), new UnityEcsDebug());

            _physicsContext = GetComponent<IPhysicsContext>();
            _physicsContext?.Init(_context.World);
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