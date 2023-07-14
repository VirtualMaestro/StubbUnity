using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit2D(Collider2D other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null || !otherView.GetEntity().IsAlive() || !Dispatcher.GetEntity().IsAlive())
                return;

            Dispatcher.World.DispatchTriggerExit2D(Dispatcher, otherView, other);
        }
    }
}