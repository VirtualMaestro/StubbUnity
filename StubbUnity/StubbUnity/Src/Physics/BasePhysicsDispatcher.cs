using UnityEngine;

namespace StubbUnity.Physics
{
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected ViewPhysics Dispatcher;
        
        private void Start()
        {
            Dispatcher = GetComponent<ViewPhysics>();
        }
    }
}