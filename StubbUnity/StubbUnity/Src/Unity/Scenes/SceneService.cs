using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Services;
using StubbUnity.Unity.Extensions;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Scenes
{
    public class SceneService : ISceneService
    {
        private readonly EcsWorld _ecsWorld;
        private readonly SceneLoader _loader;
        private readonly SceneUnloader _unloader;
        private readonly Queue<ProcessSetScenesConfig> _processingQueue;
        private readonly Dictionary<Scene, ILoadingSceneConfig> _loadedScenes;
        private Scene _interstitial;
        private bool _isInProgress;
        private bool _isLoadingDone;
        private bool _isUnloadingDone;
        
        public SceneService(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            
            _loader = new SceneLoader();
            _loader.OnSceneLoaded += _OnSceneLoaded;
            _loader.OnAllScenesLoaded += _OnAllScenesLoaded;
            
            _unloader = new SceneUnloader();
            _unloader.OnSceneUnloaded += _OnSceneUnloaded;
            _unloader.OnAllScenesUnloaded += _OnAllScenesUnloaded;

            _processingQueue = new();
            _loadedScenes = new();
        }
        
        public void Process(ProcessSetScenesConfig config)
        {
            _processingQueue.Enqueue(config);
            
            _ProcessNextConfig();
        }

        private void _ProcessNextConfig()
        {
            if (_isInProgress || _processingQueue.Count == 0)
                return;
            
            _isInProgress = true;
            
            var configInProcess = _processingQueue.Dequeue();
            
            // check given scene names for validity
            if (configInProcess.UnloadingList.Count > 0)
                _FilterOutNonValidScenes(configInProcess.UnloadingList);

            if (configInProcess.UnloadOthers)
                _CollectScenesOnStageToUnload(configInProcess);

            if (configInProcess.UnloadingList.Count >= SceneManager.loadedSceneCount)
                _CreateInterstitialScene();

            // Deactivate all current scenes
            foreach (var sceneName in configInProcess.UnloadingList)
            {
                var scene = SceneManager.GetSceneByPath(sceneName.FullName);
                scene.Deactivate();
            }
            
            _Perform(configInProcess);
        }

        private void _FilterOutNonValidScenes(List<IAssetName> unloadingList)
        {
            for (var i = unloadingList.Count - 1; i >= 0; i--)
            {
                var scene = SceneManager.GetSceneByPath(unloadingList[i].FullName);
                
                if (!scene.isLoaded || !scene.IsValid())
                    unloadingList.RemoveAt(i);
            }
        }

        private void _Perform(ProcessSetScenesConfig config)
        {
            if (!_isUnloadingDone)
                _unloader.Unload(config);
            else if (!_isLoadingDone)
                _loader.Load(config);
            else // all scenes are loaded/unloaded
            {
                if (_interstitial.path != null) //isLoaded
                    SceneManager.UnloadSceneAsync(_interstitial);

                _SetupLoadedScenes();
                
                // reset 
                _isLoadingDone = false;
                _isUnloadingDone = false;
                _isInProgress = false;
                
                // notify ecs
                _ecsWorld.NewEntity().Get<ScenesSetLoadingCompleteEvent>().ScenesSetName = config.Name;

                _ProcessNextConfig();
            }
        }

        private void _SetupLoadedScenes()
        {
            foreach (var (scene, config) in _loadedScenes)
            {
                if (config.IsMain)
                    SceneManager.SetActiveScene(scene);
                
                if (scene.GetController(out var controller))
                {
                    scene.ActivateSceneController();    
                    
                    if (config.IsActive)
                        controller.ShowContent();  
                }
                else
                    scene.Activate();
            }
            
            _loadedScenes.Clear();
        }

        private void _CollectScenesOnStageToUnload(ProcessSetScenesConfig config)
        {
            var builder = SceneName.Create;
            
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                builder.Add(scene.name, scene.path);
            }
            
            config.AddToUnload(builder.Build);
        }

        private void _CreateInterstitialScene()
        {
            _interstitial = SceneManager.CreateScene("InterstitialTempScene");
        }

        //
        private void _OnSceneLoaded(Scene scene, ILoadingSceneConfig config)
        {
            _loadedScenes.Add(scene, config);
            
            scene.Deactivate();
            
            _ecsWorld.NewEntity().Get<SceneLoadingCompleteEvent>().SceneName = scene.path;
        }

        private void _OnAllScenesLoaded(ProcessSetScenesConfig config)
        {
            _isLoadingDone = true;
            _Perform(config);
        }

        private void _OnSceneUnloaded(Scene scene, IAssetName sceneName)
        {
            _ecsWorld.NewEntity().Get<SceneUnloadingCompleteEvent>().SceneName = scene.path;
        }

        private void _OnAllScenesUnloaded(ProcessSetScenesConfig config)
        {
            _isUnloadingDone = true;
            _Perform(config);
        }

        //
        public bool HasScene(in IAssetName sceneName)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (sceneName.FullName.Equals(scene.path))
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// </summary>
    internal class SceneLoader
    {
        public event Action<Scene, ILoadingSceneConfig> OnSceneLoaded;
        public event Action<ProcessSetScenesConfig> OnAllScenesLoaded;
    
        private readonly Queue<ILoadingSceneConfig> _queue;
        private ProcessSetScenesConfig _currentSetScenesConfig;
        private ILoadingSceneConfig _currentSceneConfig;
    
        public SceneLoader()
        {
            _queue = new Queue<ILoadingSceneConfig>(5);
        }
        
        public void Load(ProcessSetScenesConfig config)
        {
            if (_currentSetScenesConfig != null)
                return;
            
            SceneManager.sceneLoaded += _SceneLoaded;
            
            _currentSetScenesConfig = config;
            
            foreach (var sceneConfig in config.LoadingList)
                _queue.Enqueue(sceneConfig);
            
            _ProcessNextConfig();
        }
    
        private void _ProcessNextConfig()
        {
            if (_queue.Count == 0)
                _AllScenesLoaded();
            else
            {
                _currentSceneConfig = _queue.Dequeue();
    
                SceneManager.LoadSceneAsync(_currentSceneConfig.Name.FullName, LoadSceneMode.Additive);
            }
        }
    
        private void _SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnSceneLoaded?.Invoke(scene, _currentSceneConfig);
            _ProcessNextConfig();
        }
    
        private void _AllScenesLoaded()
        {
            var setSceneConfig = _currentSetScenesConfig;
            _currentSetScenesConfig = null;
            _currentSceneConfig = null;
            
            SceneManager.sceneLoaded -= _SceneLoaded;

            OnAllScenesLoaded?.Invoke(setSceneConfig);
        }
    }

    internal class SceneUnloader
    {
        public event Action<Scene, IAssetName> OnSceneUnloaded;
        public event Action<ProcessSetScenesConfig> OnAllScenesUnloaded; 
    
        private readonly Queue<IAssetName> _queue;
        private ProcessSetScenesConfig _currentSetScenesConfig;
        private IAssetName _currentSceneName;
    
        public SceneUnloader()
        {
            _queue = new Queue<IAssetName>(5);
        }
        
        public void Unload(ProcessSetScenesConfig config)
        {
            if (_currentSetScenesConfig != null)
                return;
            
            SceneManager.sceneUnloaded += _SceneUnloaded;
            
            _currentSetScenesConfig = config;
            
            foreach (var sceneConfig in config.UnloadingList)
                _queue.Enqueue(sceneConfig);
            
            _ProcessNextConfig();
        }
    
        private void _ProcessNextConfig()
        {
            if (_queue.Count == 0)
                _AllScenesLoaded();
            else
            {
                _currentSceneName = _queue.Dequeue();
    
                SceneManager.UnloadSceneAsync(_currentSceneName.FullName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
        }
    
        private void _SceneUnloaded(Scene scene)
        {
            OnSceneUnloaded?.Invoke(scene, _currentSceneName);
            _ProcessNextConfig();
        }
    
        private void _AllScenesLoaded()
        {
            var setSceneConfig = _currentSetScenesConfig;
            _currentSetScenesConfig = null;
            _currentSceneName = null;
            
            SceneManager.sceneUnloaded -= _SceneUnloaded;

            OnAllScenesUnloaded?.Invoke(setSceneConfig);
        }
    }
}