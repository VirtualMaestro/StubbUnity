using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerExitDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit(Collider other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchTriggerExit(Settings, otherSettings, other);
        }
    }
}