using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            Dispatcher.DispatchCollisionEnter(other);
        }
    }
}