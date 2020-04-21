using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionExitDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit(Collision other)
        {
            Dispatcher.World.DispatchCollisionExit(Dispatcher, other.gameObject.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}