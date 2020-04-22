using StubbUnity.View;
using UnityEngine;

namespace StubbUnity.Physics
{
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected EcsViewLink Dispatcher;
        
        private void Start()
        {
            Dispatcher = GetComponent<EcsViewLink>();
        }
    }
}