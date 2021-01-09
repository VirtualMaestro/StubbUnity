using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Triggers
{
    public sealed class TriggerExitDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit(Collider other)
        {
            Dispatcher.World.DispatchTriggerExit(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}