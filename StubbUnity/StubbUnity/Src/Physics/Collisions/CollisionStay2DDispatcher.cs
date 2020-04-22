using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionStay2D(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}