using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerEnter2D(Dispatcher, other.GetComponent<IViewPhysics>(), other);
        }
    }
}