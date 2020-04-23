using System.Management.Instrumentation;
using JetBrains.Annotations;
using StubbFramework.Logging;
using StubbFramework.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Extensions
{
    public static class SceneExtension
    {
        public static bool HasController(this Scene scene) => GetController(scene) != null;
        
        [CanBeNull]
        public static ISceneController GetController(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();
            
            foreach (var go in gos)
            {
                var controller = go.GetComponent<ISceneController>();
                
                if (controller == null) continue;

                return controller;
            }
            
            return null;
        }

        /// <summary>
        /// Returns GameObject which represents a Content of the scene.
        /// If root of the scene contains more than two GameObjects it is possible to return wrong container.
        /// In this case use direct drag-n-drop set to the SceneController. 
        /// </summary>
        [CanBeNull]
        public static GameObject GetContent(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();
            
            foreach (var go in gos)
            {
                if (go.HasComponent<ISceneController>()) continue;
                if (gos.Length > 2) log.Warn($"WARNING: Scene '{scene.name}' contains more than 2 root containers!");
                
                return go;
            }
            
            throw new InstanceNotFoundException($"Content of the scene '{scene.name}' wasn't found!");
        }
    }
}