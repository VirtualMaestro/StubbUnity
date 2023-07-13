using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null)
                return;

            Dispatcher.World.DispatchCollisionEnter(Dispatcher, otherView, other);
        }
    }
}