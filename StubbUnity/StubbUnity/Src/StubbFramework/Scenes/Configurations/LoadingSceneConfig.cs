using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public class LoadingSceneConfig : ILoadingSceneConfig
    {
        public IAssetName Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }

        public LoadingSceneConfig()
        {
            IsActive = true;
            IsMain = false;
        }
        
        public ILoadingSceneConfig Clone()
        {
            var config = new LoadingSceneConfig
            {
                Name = Name.Clone(), IsActive = IsActive, IsMain = IsMain
            };

            return config;
        }
    }
}