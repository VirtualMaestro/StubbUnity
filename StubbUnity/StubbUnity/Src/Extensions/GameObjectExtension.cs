using UnityEngine;

namespace StubbUnity.Extensions
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}