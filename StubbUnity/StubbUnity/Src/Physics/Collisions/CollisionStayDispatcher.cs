using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            Dispatcher.DispatchCollisionStay(other);
        }
    }
}