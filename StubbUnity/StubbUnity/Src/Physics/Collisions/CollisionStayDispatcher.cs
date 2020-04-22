using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            Dispatcher.World.DispatchCollisionStay(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}