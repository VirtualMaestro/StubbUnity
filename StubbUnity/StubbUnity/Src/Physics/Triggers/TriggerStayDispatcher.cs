using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerStayDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay(Collider other)
        {
            Dispatcher.World.DispatchTriggerStay(Dispatcher, other.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}