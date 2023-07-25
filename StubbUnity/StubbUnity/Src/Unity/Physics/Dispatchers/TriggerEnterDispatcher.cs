using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchTriggerEnter(Settings, otherSettings, other);
        }
    }
}