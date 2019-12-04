using StubbFramework.Extensions;
using StubbFramework.Physics;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionEnter2D(Dispatcher, other.gameObject.GetComponent<IViewPhysics>(), other);
        }
    }
}