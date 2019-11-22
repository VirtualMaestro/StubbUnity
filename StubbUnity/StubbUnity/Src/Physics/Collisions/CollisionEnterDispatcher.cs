using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            Dispatcher.World.DispatchCollisionEnter(Dispatcher, other.gameObject.GetComponent<IViewPhysics>(), other);
        }
    }
}