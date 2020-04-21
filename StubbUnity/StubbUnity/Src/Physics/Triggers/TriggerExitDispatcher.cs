using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerExitDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit(Collider other)
        {
            Dispatcher.World.DispatchTriggerExit(Dispatcher, other.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}