using Leopotam.Ecs;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.StubbFramework.Physics.Components;
using StubbUnity.Unity.Physics;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class CollisionDispatcherWorldExtension
    {
        public static void DispatchTriggerEnter(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerEnter, out var entity))
                entity.Get<IsTriggerEnter>();
        }

        public static void DispatchTriggerEnter2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerEnter2d, out var entity))
                entity.Get<IsTriggerEnter2d>();
        }

        public static void DispatchTriggerStay(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerStay, out var entity))
                entity.Get<IsTriggerStay>();
        }

        public static void DispatchTriggerStay2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerStay2d, out var entity))
                entity.Get<IsTriggerStay2d>();
        }

        public static void DispatchTriggerExit(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerExit, out var entity))
                entity.Get<IsTriggerExit>();
        }

        public static void DispatchTriggerExit2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.TriggerExit2d, out var entity))
                entity.Get<IsTriggerExit2d>();
        }

        public static void DispatchCollisionEnter(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionEnter, out var entity))
                entity.Get<IsCollisionEnter>();
        }

        public static void DispatchCollisionEnter2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionEnter2d, out var entity))
                entity.Get<IsCollisionEnter2d>();
        }

        public static void DispatchCollisionStay(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionStay, out var entity))
                entity.Get<IsCollisionStay>();
        }

        public static void DispatchCollisionStay2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionStay2d, out var entity))
                entity.Get<IsCollisionStay2d>();
        }

        public static void DispatchCollisionExit(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionExit, out var entity))
                entity.Get<IsCollisionExit>();
        }

        public static void DispatchCollisionExit2D(this EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB,
            object collisionInfo)
        {
            if (_HandleCollision(world, objA, objB, collisionInfo, CollisionType.CollisionExit2d, out var entity))
                entity.Get<IsCollisionExit2d>();
        }
        
        private static bool _HandleCollision(EcsWorld world, EcsCollisionSettings objA, EcsCollisionSettings objB, object collisionInfo, CollisionType collisionType, out EcsEntity entity)
        {
            entity = EcsEntity.Null;
            
            if (CollisionManager.CanDispatch(objA.TypeId, objB.TypeId, collisionType, out var isCorrectOrder, out var hash) == false) 
                return false;

            CollisionManager.RegisterCollision(ref objA, ref objB, collisionType, isCorrectOrder, hash);
            
            entity = world.NewEntity();
            ref var collision = ref entity.Get<CollisionComp>();
            collision.ObjectA = objA;
            collision.ObjectB = objB;
            collision.Info = collisionInfo;

            return true;
        }
    }
}