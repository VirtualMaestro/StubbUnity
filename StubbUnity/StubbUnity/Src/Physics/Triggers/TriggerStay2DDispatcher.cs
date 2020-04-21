using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerStay2D(Dispatcher, other.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}