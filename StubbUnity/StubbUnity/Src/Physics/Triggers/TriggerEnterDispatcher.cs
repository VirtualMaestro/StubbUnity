using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            Dispatcher.World.DispatchTriggerEnter(Dispatcher, other.GetComponent<IViewPhysics>(), other);
        }
    }
}