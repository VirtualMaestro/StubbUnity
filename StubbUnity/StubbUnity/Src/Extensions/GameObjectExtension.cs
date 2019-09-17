using UnityEngine;

namespace StubbUnity.Extensions
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : class
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}