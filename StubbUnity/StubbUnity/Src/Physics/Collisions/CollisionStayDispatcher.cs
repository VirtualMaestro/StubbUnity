using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            Dispatcher.World.DispatchCollisionStay(Dispatcher, other.gameObject.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}