using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null)
                return;

            Dispatcher.World.DispatchTriggerEnter(Dispatcher, otherView, other);
        }
    }
}