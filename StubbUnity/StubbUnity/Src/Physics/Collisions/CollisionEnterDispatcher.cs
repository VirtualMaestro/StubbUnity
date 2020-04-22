using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            Dispatcher.World.DispatchCollisionEnter(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}