using StubbFramework;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            log.AddAppender(UnityLogAppender.logDelegate);
            IStubbContext context = GetComponent<IStubbContext>();
            log.Assert(context != null, "Context missing! Attach UnityContext to the GameObject where EntryPoint script is attached!");
            Stubb.Create(context);
            Stubb.Initialize();
        }

        private void Update()
        {
            Stubb.Run();
        }

        private void OnDestroy()
        {
            Stubb.Dispose();
        }
    }
}