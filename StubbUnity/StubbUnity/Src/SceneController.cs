using System.Linq;
using StubbUnity.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity
{
    public class SceneController : MonoBehaviour
    {
        private GameObject _content;
        private Scene _scene;

        public bool IsContentShown => _content.activeSelf;
        public string SceneName => gameObject.scene.name;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _scene = gameObject.scene;
            _content = _scene.GetRootGameObjects().First(go => go.HasComponent<SceneContentController>());
        }
        
        public void ShowContent()
        {
            if (!IsContentShown)
            {
                _content.SetActive(true);
            }
        }

        public void HideContent()
        {
            if (IsContentShown)
            {
                _content.SetActive(false);
            }
        }
    }
}