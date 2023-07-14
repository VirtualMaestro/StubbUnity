using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit2D(Collision2D other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null || !otherView.GetEntity().IsAlive() || !Dispatcher.GetEntity().IsAlive())
                return;

            Dispatcher.World.DispatchCollisionExit2D(Dispatcher, otherView, other);
        }
    }
}