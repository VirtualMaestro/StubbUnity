using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            Dispatcher.DispatchTriggerEnter(other);
        }
    }
}