using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerExitDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit(Collider other)
        {
            Dispatcher.DispatchTriggerExit(other);
        }
    }
}