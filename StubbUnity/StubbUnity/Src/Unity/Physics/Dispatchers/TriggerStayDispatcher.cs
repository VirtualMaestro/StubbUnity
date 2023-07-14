using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerStayDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay(Collider other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null || !otherView.GetEntity().IsAlive() || !Dispatcher.GetEntity().IsAlive())
                return;

            Dispatcher.World.DispatchTriggerStay(Dispatcher, otherView, other);
        }
    }
}