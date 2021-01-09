using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerStayDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay(Collider other)
        {
            Dispatcher.World.DispatchTriggerStay(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}