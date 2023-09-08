using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StubbUnity.Unity.Physics;
using StubbUnity.Unity.Utils;

namespace StubbUnity.StubbFramework.Physics
{
    public static class CollisionManager
    {
        /// <summary>
        /// Key - is hash of two typeIds (typeIdA & typeIdB).
        /// Value - is tuple where first value is typeIdA (it allows to check order), and the second one is bitmask (for checking Collision types)
        /// </summary>
        private static readonly Dictionary<int, (int, int)> CollisionPairsTable = new();
        
        /// <summary>
        /// Key - is collision pair hash.
        /// Value - is mask with CollisionType values.
        /// </summary>
        private static readonly Dictionary<int, int> RegisterCollisionTable = new();
                
        /// <summary>
        /// Returns number of registered collision pairs without taking into account how many CollisionType's between them.
        /// </summary>
        public static int NumCollisionPairs() => CollisionPairsTable.Count;
        
         /// <summary>
        /// Add two uniques ids (ints) as collision pair.
        /// Ids should be > 0.
        /// </summary>
        public static void AddCollisionPair(int typeIdA, int typeIdB, CollisionType collisionType)
        {
            var hash = _GetHash(typeIdA, typeIdB);

            if (CollisionPairsTable.TryGetValue(hash, out var pair))
            {
                if (BitMask.IsSet(pair.Item2, (int)collisionType))
                    return;
                
                pair.Item2 = BitMask.Set(pair.Item2, (int)collisionType);
                CollisionPairsTable[hash] = pair;
            }
            else
            {
				// we are storing first param id of the first collider to know the order (which one registered first), and collision type for this pair.
                (int, int) newPair = (typeIdA, BitMask.Set(0, (int) collisionType));
                CollisionPairsTable.Add(hash, newPair);
            }
        }

        public static void EndPhysicsFrame()
        {
            RegisterCollisionTable.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterCollision(ref EcsCollisionSettings objA, ref EcsCollisionSettings objB, CollisionType collisionType, bool isCorrectOrder, int hash)
        {
            if (!isCorrectOrder)
                (objA, objB) = (objB, objA);

            if (RegisterCollisionTable.TryGetValue(hash, out var collisionMask))
                RegisterCollisionTable[hash] = BitMask.Set(collisionMask, (int)collisionType);
            else
                RegisterCollisionTable.Add(hash, BitMask.Set(0, (int)collisionType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanDispatch(int typeIdA, int typeIdB, CollisionType collisionType, out bool isCorrectOrder, out int hash)
        {
            isCorrectOrder = true;
            hash = _GetHash(typeIdA, typeIdB);

            if (!CollisionPairsTable.TryGetValue(hash, out var pair) || !BitMask.IsSet(pair.Item2, (int)collisionType))
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