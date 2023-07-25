using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay2D(Collider2D other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchTriggerStay2D(Settings, otherSettings, other);
        }
    }
}