using StubbFramework.Extensions;
using StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Physics.Triggers
{
    public sealed class TriggerExitDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit(Collider other)
        {
            Dispatcher.World.DispatchTriggerExit(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}