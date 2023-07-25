using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit2D(Collider2D other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchTriggerExit2D(Settings, otherSettings, other);
        }
    }
}