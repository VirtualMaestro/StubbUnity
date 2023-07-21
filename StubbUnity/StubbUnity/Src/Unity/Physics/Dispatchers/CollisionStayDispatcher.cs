using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null)
                return;

            Dispatcher.World.DispatchCollisionStay(Dispatcher, otherView, other);
        }
    }
}