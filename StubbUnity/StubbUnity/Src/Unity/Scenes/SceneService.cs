using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Services;
using StubbUnity.Unity.Extensions;
using StubbUnity.Unity.Utils;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Scenes
{
    public class SceneService : ISceneService
    {
        private readonly EcsWorld _ecsWorld;
        private readonly SceneLoader _loader;
        private readonly SceneUnloader _unloader;
        private readonly Queue<ScenesLoadingConfiguration> _processingQueue;
        private readonly Dictionary<Scene, ILoadingSceneConfig> _loadedScenes;
        private Scene _interstitialScene;
        private bool _isInProgress;
        private bool _isLoadingDone;
        private bool _isUnloadingDone;
        private ScenesLoadingConfiguration _configInProcess;
        private CoroutineManager.ICJob _interstitialDelayJob;
        
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
        
        public void Process(ScenesLoadingConfiguration loadingConfiguration)
        {
            _processingQueue.Enqueue(loadingConfiguration);
            
            _ProcessNextConfig();
        }

        private void _ProcessNextConfig()
        {
            if (_isInProgress || _processingQueue.Count == 0)
                return;
            
            _isInProgress = true;
            
            var configInProcess = _processingQueue.Dequeue();
            _configInProcess = configInProcess;
            
            // check given scene names for validity
            if (configInProcess.Unloadings.Count > 0)
                _FilterOutNonValidScenes(configInProcess.Unloadings);

            if (configInProcess.IsUnloadOthers)
                _CollectScenesOnStageToUnload(configInProcess);

            _DeactivateAllCurrentScenes(configInProcess);
            
            if (configInProcess.HasInterstitial)
            {
                // There is an interstitial scene that has to be loaded
                _LoadInterstitialScene(configInProcess.Interstitial.FullName);
            }
            else
            {
                if (configInProcess.Unloadings.Count >= SceneManager.loadedSceneCount)
                    _CreateInterstitialScene();
            
                _Perform(configInProcess);
            }
        }

        private void _LoadInterstitialScene(string interstitialFullSceneName)
        {
            SceneManager.sceneLoaded += _OnInterstitialSceneLoaded;
            SceneManager.LoadSceneAsync(interstitialFullSceneName, LoadSceneMode.Additive);
        }

        private void _UnloadInterstitialScene()
        {
            _SetupLoadedScenes();
            
            _interstitialScene.Deactivate();
            
            SceneManager.sceneUnloaded += _OnInterstitialSceneUnloaded;
            SceneManager.UnloadSceneAsync(_interstitialScene);
        }

        private void _OnInterstitialSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= _OnInterstitialSceneLoaded;

            _interstitialScene = scene;
            
            // Show interstitial scene for amount of seconds that is defined in InterstitialDuration
            if (_configInProcess.InterstitialDuration > 0)
                _interstitialDelayJob = CoroutineManager.OnceSeconds(_OnInterstitialDelayComplete, _configInProcess.InterstitialDuration);
            
            _Perform(_configInProcess);
        }

        private void _OnInterstitialSceneUnloaded(Scene scene)
        {
            SceneManager.sceneUnloaded -= _OnInterstitialSceneUnloaded;
            _FinalizingLoadingConfiguration();
        }

        private void _OnInterstitialDelayComplete()
        {
            _interstitialDelayJob.Dispose();
            _interstitialDelayJob = null;

            if (_isLoadingDone && _isUnloadingDone)
                _UnloadInterstitialScene();
        }

        private void _DeactivateAllCurrentScenes(ScenesLoadingConfiguration configInProcess)
        {
            foreach (var sceneName in configInProcess.Unloadings)
            {
                var scene = SceneManager.GetSceneByPath(sceneName.FullName);
                scene.Deactivate();
            }
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

        private void _Perform(ScenesLoadingConfiguration loadingConfiguration)
        {
            if (!_isUnloadingDone)
                _unloader.Unload(loadingConfiguration);
            else if (!_isLoadingDone)
                _loader.Load(loadingConfiguration);
            else // all scenes are loaded/unloaded
            {
                // if there is a loaded interstitial scene and there is a delay for it
                if (_interstitialScene.isLoaded)
                {
                    if (_interstitialDelayJob == null)
                        _UnloadInterstitialScene();
                }
                else
                {
                    _SetupLoadedScenes();
                    _FinalizingLoadingConfiguration();
                }
            }
        }

        private void _FinalizingLoadingConfiguration()
        {
            // reset 
            _isLoadingDone = false;
            _isUnloadingDone = false;
            _isInProgress = false;
                
            // notify ecs
            _ecsWorld.NewEntity().Get<ScenesLoadingConfigurationCompleteEvent>().ConfigurationName = _configInProcess.Name;

            _configInProcess.Dispose();
            _configInProcess = null;
                
            //
            _ProcessNextConfig();
        }

        private void _SetupLoadedScenes()
        {
            foreach (var pair in _loadedScenes)
            {
                var config = pair.Value;
                var scene = pair.Key;
                
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

        private void _CollectScenesOnStageToUnload(ScenesLoadingConfiguration loadingConfiguration)
        {
            var builder = SceneName.Create;
            
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                
                if (!loadingConfiguration.HasException(scene.path))
                    builder.Add(scene.name, scene.path);
            }
            
            loadingConfiguration.AddToUnload(builder.Build);
        }

        private void _CreateInterstitialScene()
        {
            _interstitialScene = SceneManager.CreateScene("InterstitialTempScene");
        }

        //
        private void _OnSceneLoaded(Scene scene, ILoadingSceneConfig config)
        {
            _loadedScenes.Add(scene, config);
            
            scene.Deactivate();
            
            _ecsWorld.NewEntity().Get<SceneLoadingCompleteEvent>().SceneName = scene.path;
        }

        private void _OnAllScenesLoaded(ScenesLoadingConfiguration loadingConfiguration)
        {
            _isLoadingDone = true;
            _Perform(loadingConfiguration);
        }

        private void _OnSceneUnloaded(Scene scene, IAssetName sceneName)
        {
            _ecsWorld.NewEntity().Get<SceneUnloadingCompleteEvent>().SceneName = scene.path;
        }

        private void _OnAllScenesUnloaded(ScenesLoadingConfiguration loadingConfiguration)
        {
            _ecsWorld.NewEntity().Get<ScenesUnloadingConfigurationCompleteEvent>().ConfigurationName = loadingConfiguration.Name;
            _isUnloadingDone = true;
            _Perform(loadingConfiguration);
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
        public event Action<ScenesLoadingConfiguration> OnAllScenesLoaded;
    
        private readonly Queue<ILoadingSceneConfig> _queue;
        private ScenesLoadingConfiguration _currentScenesLoadingConfiguration;
        private ILoadingSceneConfig _currentSceneConfig;
    
        public SceneLoader()
        {
            _queue = new Queue<ILoadingSceneConfig>(5);
        }
        
        public void Load(ScenesLoadingConfiguration loadingConfiguration)
        {
            if (_currentScenesLoadingConfiguration != null)
                return;
            
            SceneManager.sceneLoaded += _SceneLoaded;
            
            _currentScenesLoadingConfiguration = loadingConfiguration;
            
            foreach (var sceneConfig in loadingConfiguration.Loadings)
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
            var setSceneConfig = _currentScenesLoadingConfiguration;
            _currentScenesLoadingConfiguration = null;
            _currentSceneConfig = null;
            
            SceneManager.sceneLoaded -= _SceneLoaded;

            OnAllScenesLoaded?.Invoke(setSceneConfig);
        }
    }

    internal class SceneUnloader
    {
        public event Action<Scene, IAssetName> OnSceneUnloaded;
        public event Action<ScenesLoadingConfiguration> OnAllScenesUnloaded; 
    
        private readonly Queue<IAssetName> _queue;
        private ScenesLoadingConfiguration _currentScenesLoadingConfiguration;
        private IAssetName _currentSceneName;
    
        public SceneUnloader()
        {
            _queue = new Queue<IAssetName>(5);
        }
        
        public void Unload(ScenesLoadingConfiguration loadingConfiguration)
        {
            if (_currentScenesLoadingConfiguration != null)
                return;
            
            SceneManager.sceneUnloaded += _SceneUnloaded;
            
            _currentScenesLoadingConfiguration = loadingConfiguration;
            
            foreach (var sceneConfig in loadingConfiguration.Unloadings)
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
            var scenesLoadingConfig = _currentScenesLoadingConfiguration;
            _currentScenesLoadingConfiguration = null;
            _currentSceneName = null;
            
            SceneManager.sceneUnloaded -= _SceneUnloaded;

            OnAllScenesUnloaded?.Invoke(scenesLoadingConfig);
        }
    }
}