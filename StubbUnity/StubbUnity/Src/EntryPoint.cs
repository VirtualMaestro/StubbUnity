using StubbFramework;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private void Start()
        {
            log.AddAppender(UnityLogAppender.logDelegate);
            _context = GetComponent<IStubbContext>();
            log.Assert(_context != null, "Context is missing! Attach UnityContext to the GameObject where EntryPoint script is attached!");
            _context.Init();
        }

        private void Update()
        {
            _context.Run();
        }

        private void OnDestroy()
        {
            _context.Dispose();
        }
    }
}