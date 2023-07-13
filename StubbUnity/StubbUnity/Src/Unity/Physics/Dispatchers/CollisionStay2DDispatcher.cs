using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay2D(Collision2D other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null)
                return;

            Dispatcher.World.DispatchCollisionStay2D(Dispatcher, otherView, other);
        }
    }
}