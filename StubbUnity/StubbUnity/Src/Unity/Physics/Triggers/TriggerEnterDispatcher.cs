using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            Dispatcher.World.DispatchTriggerEnter(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}