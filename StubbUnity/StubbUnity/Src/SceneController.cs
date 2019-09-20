using System;
using System.Runtime.CompilerServices;
using StubbFramework.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        private ISceneContentController _content;
        private Scene _scene;

        public string SceneName => _scene.name;
        public bool IsContentActive => _content.IsActive;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _scene = gameObject.scene;
            _content = _GetContentController();
        }
        
        public void ShowContent()
        {
            _content.Show();
        }

        public void HideContent()
        {
            _content.Hide();
        }

        public void Destroy()
        {
            _content.Destroy();
            _content = null;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private ISceneContentController _GetContentController()
        {
            var gObjects = _scene.GetRootGameObjects();
            foreach (var gObj in gObjects)
            {
                ISceneContentController contentController = gObj.GetComponent<ISceneContentController>();
                
                if (contentController != null)
                {
                    return contentController;
                }
            }
            
            throw new Exception("SceneController, SceneName: " + SceneName + " Content doesn't contain ISceneContentController component!'");
        }
    }
}