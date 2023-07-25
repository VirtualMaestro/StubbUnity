using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit2D(Collision2D other)
        {
            var otherSettings = other.gameObject.GetComponent<EcsCollisionSettings>();
            
            if (otherSettings == null)
                return;

            World.DispatchCollisionExit2D(Settings, otherSettings, other);
        }
    }
}