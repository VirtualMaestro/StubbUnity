using UnityEngine;

namespace StubbUnity.Physics
{
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected EcsViewPhysics Dispatcher;
        
        private void Start()
        {
            Dispatcher = GetComponent<EcsViewPhysics>();
        }
    }
}