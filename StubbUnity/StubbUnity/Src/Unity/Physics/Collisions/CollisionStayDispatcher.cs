using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Collisions
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            Dispatcher.World.DispatchCollisionStay(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}