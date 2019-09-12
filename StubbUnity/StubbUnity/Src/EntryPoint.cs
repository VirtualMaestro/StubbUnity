using StubbFramework;
using StubbUnity.Debugging;
using StubbUnity.Logging;
using UnityEngine;

namespace StubbUnity
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private EcsFeature _rootFeature;
        private Stubb _stubb;
        
        private void Start()
        {
            log.AddAppender(UnityLogAppender.logDelegate);
            _stubb = Stubb.Instance;
            _stubb.Add(_rootFeature);
            _stubb.Initialize(new UnityEcsDebug());
        }

        private void Update()
        {
            _stubb.Update();
        }

        private void OnDestroy()
        {
            _stubb.Dispose();
        }
    }
}