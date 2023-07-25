using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchCollisionEnter2D(Settings, otherSettings, other);
        }
    }
}