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
            Stubb.Create(context);
            Stubb.Initialize();
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