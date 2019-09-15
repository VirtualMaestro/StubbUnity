using StubbFramework;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private UnityContext _context;
        
        private void Start()
        {
            log.AddAppender(UnityLogAppender.logDelegate);
            Stubb.Create(_context);
        }

        private void Update()
        {
            Stubb.Update();
        }

        private void OnDestroy()
        {
            Stubb.Dispose();
        }
    }
}