using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionExitDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit(Collision other)
        {
            Dispatcher.DispatchCollisionExit(other);
        }
    }
}