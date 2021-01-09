using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Extensions
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : class
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// Returns true if GameObject contains component of IEcsViewLink
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool HasView(this GameObject gameObject)
        {
            return gameObject.GetComponent<IEcsViewLink>() != null;
        }
    }
}