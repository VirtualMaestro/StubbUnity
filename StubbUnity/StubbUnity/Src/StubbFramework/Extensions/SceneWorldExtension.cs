using System.Collections.Generic;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class SceneWorldExtension
    {
        /// <summary>
        /// Loads a scene by a name.
        /// It implies that scene will be activated immediately, and will be main. 
        /// </summary>
        public static void LoadScene(this EcsWorld world, IAssetName sceneName, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToLoad(sceneName);
            
            _LoadScenesInternal(world, config);
        }

        /// <summary>
        /// Loads a scene by a name.
        /// It implies that scene will be activated immediately, and will be main. 
        /// </summary>
        public static void LoadScene(this EcsWorld world, IAssetName sceneName, IAssetName unloadScene, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToLoad(sceneName)
                .AddToUnload(unloadScene);
            
            _LoadScenesInternal(world, config);
        }
        
        /// <summary>
        /// Loads a scene by a name and unload all others scene.
        /// </summary>
        public static void LoadSceneOnly(this EcsWorld world, IAssetName sceneName, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToLoad(sceneName, true, true)
                .UnloadOthers();

            _LoadScenesInternal(world, config);
        }

        /// <summary>
        /// Loads list of scenes by their names and unload list scenes by their names.
        /// </summary>
        public static void LoadScenesOnly(this EcsWorld world, List<IAssetName> loadSceneNames, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName).UnloadOthers();
            
            foreach (var sceneName in loadSceneNames)
                config.AddToLoad(sceneName);

            _LoadScenesInternal(world, config);
        }

        /// <summary>
        /// Loads list of scenes by their names and unload list scenes by their names.
        /// </summary>
        public static void LoadScenes(this EcsWorld world, List<IAssetName> loadSceneNames, List<IAssetName> unloadSceneNames,
            string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToLoad(loadSceneNames)
                .AddToUnload(unloadSceneNames);

            _LoadScenesInternal(world, config);
        }

        /// <summary>
        /// Unload scene by the given name.
        /// </summary>
        public static void UnloadScene(this EcsWorld world, IAssetName sceneName, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToUnload(sceneName);
            
            _LoadScenesInternal(world, config);
        }

        /// <summary>
        /// Unload list of scenes.
        /// Names of scenes should be specified in full name format (path+name).
        /// UnloadScenesComponent will be sent.
        /// </summary>
        public static void UnloadScenes(this EcsWorld world, List<IAssetName> sceneNames, string configName = null)
        {
            var config = ScenesLoadingConfiguration.Get(configName)
                .AddToUnload(sceneNames);
            
            _LoadScenesInternal(world, config);
        }

        public static void LoadScenes(this EcsWorld world, ScenesLoadingConfiguration configuration)
        {
            _LoadScenesInternal(world, configuration.Clone());
        }
        
        private static void _LoadScenesInternal(this EcsWorld world, ScenesLoadingConfiguration configuration)
        {
            world.NewEntity().Get<ProcessScenesEvent>().Configuration = configuration;
        }

        /// <summary>
        /// Activate scene by its name.
        /// </summary>
        public static void ActivateScene(this EcsWorld world, IAssetName sceneName, bool isMain = false)
        {
            ref var activateScene = ref world.NewEntity().Get<ActivateSceneEvent>();
            activateScene.SceneName = sceneName;
            activateScene.IsMain = isMain;
        }

        /// <summary>
        /// Deactivate a scene by its name.
        /// </summary>
        public static void DeactivateScene(this EcsWorld world, IAssetName sceneName)
        {
            world.NewEntity().Get<DeactivateSceneEvent>().SceneName = sceneName;
        }
    }
}