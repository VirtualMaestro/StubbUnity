#if DEVELOPMENT_BUILD 
using System;
#endif

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Pooling;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public class ScenesLoadingConfiguration
    {
        public string Name { get; private set; }
        public List<ILoadingSceneConfig> Loadings { get; }
        public List<IAssetName> Unloadings { get; }
        public IAssetName Interstitial { get; private set; }
       
        /// <summary>
        /// Duration in seconds.
        /// </summary>
        public float InterstitialDuration { get; private set; }

        public bool HasException(IAssetName sceneName) => HasException(sceneName.FullName);
        public bool HasException(string fullSceneName) => _exceptions.Contains(fullSceneName);
        public bool HasInterstitial => Interstitial != null;
        public bool IsUnloadOthers { get; private set; }

        private readonly HashSet<string> _exceptions;

        private ScenesLoadingConfiguration()
        {
            Loadings = new List<ILoadingSceneConfig>(4);
            Unloadings = new List<IAssetName>(4);
            _exceptions = new HashSet<string>(4);
        }

        public ScenesLoadingConfiguration UnloadOthers()
        {
            IsUnloadOthers = true;
            return this;
        }

        public ScenesLoadingConfiguration SetInterstitial(IAssetName interstitialScene, float interstitialDuration = 0)
        {
            Interstitial = interstitialScene;
            InterstitialDuration = interstitialDuration;
            return this;
        }
        
        public ScenesLoadingConfiguration AddToLoad(IAssetName sceneName, bool isActive = true, bool isMain = false)
        {
            _ValidateSceneNames(sceneName, "AddToLoad");
            
            var config = new LoadingSceneConfig {Name = sceneName, IsActive = isActive, IsMain = isMain};
            AddToLoad(config);
            return this;
        }
        
        /// <summary>
        /// All added scenes will be with a status 'isActive'.
        /// </summary>
        public ScenesLoadingConfiguration AddToLoad(List<IAssetName> sceneNames, bool isActive = true)
        {
            _ValidateSceneNames(sceneNames, "AddToLoad");
            
            foreach (var sceneName in sceneNames)
                AddToLoad(sceneName, isActive);                

            return this;
        }
        
        public ScenesLoadingConfiguration AddToLoad(ILoadingSceneConfig config)
        {
            _ValidateConfigs(config, "AddToLoad");
            
            Loadings.Add(config);
            return this;
        }

        public ScenesLoadingConfiguration AddToLoad(List<ILoadingSceneConfig> configs)
        {
            _ValidateConfigs(configs, "AddToLoad");
            
            foreach (var config in configs)
                AddToLoad(config);
            
            return this;
        }

        public ScenesLoadingConfiguration AddToUnload(IAssetName sceneName)
        {
            _ValidateSceneNames(sceneName, "AddToUnload");
            
            Unloadings.Add(sceneName);
            return this;
        }
        
        public ScenesLoadingConfiguration AddToUnload(List<IAssetName> sceneNames)
        {
            _ValidateSceneNames(sceneNames, "AddToUnload");
            
            foreach (var sceneName in sceneNames)
                AddToUnload(sceneName);
            
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(string sceneFullName)
        {
            _ValidateSceneNames(sceneFullName, "AddToExcept");
            
            _exceptions.Add(sceneFullName);
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(IAssetName sceneName)
        {
            _ValidateSceneNames(sceneName, "AddToExcept");
            
            AddToExcept(sceneName.FullName);
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(List<IAssetName> sceneNames)
        {
            _ValidateSceneNames(sceneNames, "AddToExcept");
            
            foreach (var sceneName in sceneNames)
                AddToExcept(sceneName);
            
            return this;
        }

        public void Dispose()
        {
            Loadings.Clear();
            Unloadings.Clear();
            _exceptions.Clear();
            
            Interstitial = null;

            InterstitialDuration = 0;
            IsUnloadOthers = false;
            Name = null;
            
            Pools.I.Get<ScenesLoadingConfiguration>().Put(this);
        }

        public ScenesLoadingConfiguration Clone()
        {
            var config = Get(Name);
            
            config.AddToLoad(Loadings);
            config.AddToUnload(Unloadings);

            foreach (var sceneName in _exceptions)
                config.AddToExcept(sceneName);

            config.SetInterstitial(Interstitial, InterstitialDuration);
            config.IsUnloadOthers = IsUnloadOthers;    
            
            return config;
        }

        //
        public static ScenesLoadingConfiguration Get(string name = null)
        {
            var config = Pools.I.Get<ScenesLoadingConfiguration>().Get();
            config.Name = name;
            return config;
        }
     
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _ValidateSceneNames(string sceneName, string prefix)
        {
#if DEVELOPMENT_BUILD 
            if (string.IsNullOrEmpty(sceneName))
                throw new Exception($"{prefix} : World.LoadScene(s): Given scene name for loading/unloading is null!");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _ValidateSceneNames(IAssetName sceneName, string prefix)
        {
#if DEVELOPMENT_BUILD 
            if (sceneName == null)
                throw new Exception($"{prefix}: World.LoadScene(s): Given scene name for loading/unloading is null!");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _ValidateSceneNames(List<IAssetName> loadSceneNames, string prefix)
        {
#if DEVELOPMENT_BUILD
            if (loadSceneNames == null) 
                throw new Exception($"{prefix}: World.LoadScene(s): Given list of scene names for loading/unloading is null!");
            
            foreach (var sceneName in loadSceneNames)
                _ValidateSceneNames(sceneName, prefix);                
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _ValidateConfigs(ILoadingSceneConfig config, string prefix)
        {
#if DEVELOPMENT_BUILD 
            if (config == null)
                throw new Exception($"{prefix}: World.LoadScene(s): Given scene config for loading/unloading is null!");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _ValidateConfigs(List<ILoadingSceneConfig> configs, string prefix)
        {
#if DEVELOPMENT_BUILD
            if (configs == null) 
                throw new Exception($"{prefix}: World.LoadScene(s): Given list of scene configs for loading/unloading is null!");
            
            foreach (var config in configs)
                _ValidateConfigs(config, prefix);                
#endif
        }
    }
}