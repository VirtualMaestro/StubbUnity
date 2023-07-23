using System.Collections.Generic;
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
            var config = new LoadingSceneConfig {Name = sceneName, IsActive = isActive, IsMain = isMain};
            AddToLoad(config);
            return this;
        }
        
        /// <summary>
        /// All added scenes will be with a status 'isActive'.
        /// </summary>
        public ScenesLoadingConfiguration AddToLoad(List<IAssetName> sceneNames, bool isActive = true)
        {
            if (sceneNames == null || sceneNames.Count == 0) 
                return this;
            
            foreach (var sceneName in sceneNames)
                AddToLoad(sceneName, isActive);                

            return this;
        }
        
        public ScenesLoadingConfiguration AddToLoad(ILoadingSceneConfig config)
        {
            Loadings.Add(config);
            return this;
        }

        public ScenesLoadingConfiguration AddToLoad(List<ILoadingSceneConfig> configs)
        {
            if (configs == null || configs.Count == 0) 
                return this;
            
            foreach (var config in configs)
                AddToLoad(config);
            
            return this;
        }

        public ScenesLoadingConfiguration AddToUnload(IAssetName sceneName)
        {
            Unloadings.Add(sceneName);
            return this;
        }
        
        public ScenesLoadingConfiguration AddToUnload(List<IAssetName> sceneNames)
        {
            if (sceneNames == null || sceneNames.Count == 0) 
                return this;
            
            foreach (var sceneName in sceneNames)
                AddToUnload(sceneName);
            
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(string sceneFullName)
        {
            _exceptions.Add(sceneFullName);
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(IAssetName sceneName)
        {
            AddToExcept(sceneName.FullName);
            return this;
        }

        public ScenesLoadingConfiguration AddToExcept(List<IAssetName> sceneNames)
        {
            if (sceneNames == null || sceneNames.Count == 0) 
                return this;
            
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
    }
}