using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchTriggerEnter2D(Settings, otherSettings, other);
        }
    }
}