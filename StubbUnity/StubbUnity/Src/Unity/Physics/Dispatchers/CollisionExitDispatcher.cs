using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionExitDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit(Collision other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null || !otherView.GetEntity().IsAlive() || !Dispatcher.GetEntity().IsAlive())
                return;

            Dispatcher.World.DispatchCollisionExit(Dispatcher, otherView, other);
        }
    }
}