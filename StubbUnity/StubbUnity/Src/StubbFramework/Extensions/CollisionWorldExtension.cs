using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.StubbFramework.Physics.Components;
using StubbUnity.Unity.Physics;
using StubbUnity.Unity.Utils;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class CollisionWorldExtension
    {
        /// <summary>
        /// Key - is hash of two typeIds (typeIdA & typeIdB).
        /// Value - is tuple where first value is typeIdA (it allows to check order), and the second one is bitmask (for checking Collision types)
        /// </summary>
        private static readonly Dictionary<int, (int, int)> CollisionTable = new();
        
        /// <summary>
        /// Key - is collision pair hash.
        /// Value - is mask with CollisionType values.
        /// </summary>
        private static readonly Dictionary<int, int> RegisterCollisionTable = new();

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
            
            if (_CanDispatch(objA.TypeId, objB.TypeId, collisionType, out var isCorrectOrder, out var hash) == false) 
                return false;

            _RegisterCollision(ref objA, ref objB, collisionType, isCorrectOrder, hash);
            
            entity = world.NewEntity();
            ref var collision = ref entity.Get<CollisionComp>();
            collision.ObjectA = objA;
            collision.ObjectB = objB;
            collision.Info = collisionInfo;

            return true;
        }

        /// <summary>
        /// Add two uniques ids (ints) as collision pair.
        /// Ids should be > 0.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="typeIdA"></param>
        /// <param name="typeIdB"></param>
        /// <param name="collisionType"></param>
        public static void AddCollisionPair(this EcsWorld world, int typeIdA, int typeIdB, CollisionType collisionType)
        {
            var hash = _GetHash(typeIdA, typeIdB);

            if (CollisionTable.TryGetValue(hash, out var pair))
            {
                if (BitMask.IsSet(pair.Item2, (int)collisionType))
                    return;
                
                BitMask.Set(pair.Item2, (int)collisionType);
                CollisionTable[hash] = pair;
            }
            else
            {
                (int, int) newPair = (typeIdA, BitMask.Set(0, (int) collisionType));
                CollisionTable.Add(hash, newPair);
            }
        }

        public static void EndPhysicsFrame(this EcsWorld world)
        {
            RegisterCollisionTable.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _RegisterCollision(ref EcsCollisionSettings objA, ref EcsCollisionSettings objB, CollisionType collisionType, bool isCorrectOrder, int hash)
        {
            if (!isCorrectOrder)
                (objA, objB) = (objB, objA);

            if (RegisterCollisionTable.TryGetValue(hash, out var collisionMask))
                RegisterCollisionTable[hash] = BitMask.Set(collisionMask, (int)collisionType);
            else
                RegisterCollisionTable.Add(hash, BitMask.Set(0, (int)collisionType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _CanDispatch(int typeIdA, int typeIdB, CollisionType collisionType, out bool isCorrectOrder, out int hash)
        {
            isCorrectOrder = true;
            hash = _GetHash(typeIdA, typeIdB);

            if (!CollisionTable.TryGetValue(hash, out var pair) || !BitMask.IsSet(pair.Item2, (int)collisionType))
                return false;
            
            isCorrectOrder = pair.Item1 == typeIdA;
            
            if (RegisterCollisionTable.TryGetValue(hash, out var collisionMask) && BitMask.IsSet(collisionMask, (int)collisionType))
                return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int _GetHash(int byte1, int byte2)
        {
            return byte1 > byte2 ? byte2 | (byte1 << 16) : byte1 | (byte2 << 16);
        }
    }
}