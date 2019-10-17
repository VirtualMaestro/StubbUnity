using System.Collections.Generic;
using System.Text.RegularExpressions;
using StubbFramework.Scenes;

namespace StubbUnity.Scenes
{
    public class SceneName : AbstractSceneName
    {
        private static readonly Regex NormalizePathRegex = new Regex(@"^\s*/|Assets/|\w+.unity", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        
        public static SceneNameBuilder Create => new SceneNameBuilder();

        public SceneName(string name, string path = null) : base(name, path)
        {}

        protected override string FormatName(string sceneName)
        {
            sceneName = base.FormatName(sceneName);
            sceneName = NormalizePathRegex.Replace(sceneName, string.Empty);

            return sceneName;
        }

        protected override string FormatPath(string path)
        {
            path = base.FormatPath(path);
            path = path.Replace("\\", "/");
            path = NormalizePathRegex.Replace(path, string.Empty);
            path = (path[path.Length - 1] != '/') ? (path + "/") : path;
            
            return path;
        }

        protected override string FormatFullName(string name, string path)
        {
            return "Assets/" + path + name + ".unity";
        }

        public override ISceneName Clone()
        {
            return new SceneName(Name, Path);
        }
    }
    
    public class SceneNameBuilder
    {
        private readonly IList<ISceneName> _sceneNames;
        
        public IList<ISceneName> Build => _sceneNames;
        
        public SceneNameBuilder()
        {
            _sceneNames = new List<ISceneName>();
        }

        public SceneNameBuilder Add(string sceneName, string scenePath = null)
        {
            _sceneNames.Add(new SceneName(sceneName, scenePath));
            return this;
        }
    }
}