using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Triggers
{
    public sealed class TriggerStayDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay(Collider other)
        {
            Dispatcher.World.DispatchTriggerStay(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}