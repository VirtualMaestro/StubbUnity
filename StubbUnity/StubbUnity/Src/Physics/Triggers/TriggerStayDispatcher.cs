using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerStayDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay(Collider other)
        {
            Dispatcher.DispatchTriggerStay(other);
        }
    }
}