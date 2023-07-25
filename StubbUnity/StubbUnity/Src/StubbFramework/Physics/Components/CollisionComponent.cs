using StubbUnity.Unity.Physics;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// Contains an information about a collision.
    /// This component will be removed at the end of physics frame.
    /// </summary>
    public struct CollisionComponent 
    {
        public EcsCollisionSettings ObjectA;
        public EcsCollisionSettings ObjectB;
        public object Info;
    }
}