using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchCollisionEnter(Settings, otherSettings, other);
        }
    }
}