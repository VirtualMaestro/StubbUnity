using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerExit2D(Dispatcher, other.GetComponent<IViewPhysics>(), other);
        }
    }
}