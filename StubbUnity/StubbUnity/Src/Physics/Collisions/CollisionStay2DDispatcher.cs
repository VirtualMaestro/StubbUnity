using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionStay2D(Dispatcher, other.gameObject.GetComponent<IEcsViewPhysics>(), other);
        }
    }
}