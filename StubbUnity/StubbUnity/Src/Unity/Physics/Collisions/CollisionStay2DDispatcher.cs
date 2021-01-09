using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Collisions
{
    public class CollisionStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionStay2D(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}