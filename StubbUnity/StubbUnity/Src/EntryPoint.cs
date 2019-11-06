using Leopotam.Ecs;
using StubbFramework;
using StubbUnity.Contexts;
using StubbUnity.Debugging;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private IFixedUpdateContext _fixedUpdateContext;
        
        private void Start()
        {
            log.AddAppender(UnityLogAppender.logDelegate);
            _context = GetComponent<IStubbContext>();
            log.Assert(_context != null, "Context is missing! Attach UnityContext to the GameObject where EntryPoint script is attached!");
            _context?.Init(new EcsWorld(), new UnityEcsDebug());

            _fixedUpdateContext = GetComponent<IFixedUpdateContext>();
            _fixedUpdateContext?.Init(_context.World);
        }

        private void Update()
        {
            _context.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateContext?.Run();
        }

        private void OnDestroy()
        {
            _context.Dispose();
            _fixedUpdateContext?.Dispose();
        }
    }
}