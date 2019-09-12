using UnityEngine;

namespace StubbUnity
{
    public class SceneController : MonoBehaviour
    {
        private uint MAX_OBJECTS_ON_SCENE = 2;
    
        private GameObject _sceneContentRoot;
        private void Start()
        {
            
        }
        
        public void ShowContent()
        {
            if (!IsContentShown)
            {
                _sceneContentRoot.SetActive(true);
            }
        }

        public void HideContent()
        {
            if (IsContentShown)
            {
                _sceneContentRoot.SetActive(false);
            }
        }

        public bool IsContentShown => _sceneContentRoot.activeSelf;

        private string SceneName => gameObject.scene.name;
    }
}