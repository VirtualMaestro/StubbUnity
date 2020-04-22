using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Collisions
{
    public class CollisionExitDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit(Collision other)
        {
            Dispatcher.World.DispatchCollisionExit(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}