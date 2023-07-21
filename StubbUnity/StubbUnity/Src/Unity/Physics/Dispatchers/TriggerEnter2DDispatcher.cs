using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            var otherView = other.gameObject.GetComponent<IEcsViewLink>();
            
            if (otherView == null)
                return;

            Dispatcher.World.DispatchTriggerEnter2D(Dispatcher, otherView, other);
        }
    }
}