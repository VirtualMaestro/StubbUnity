using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionExit2D(Dispatcher, other.gameObject.GetComponent<IViewPhysics>(), other);
        }
    }
}