using Leopotam.Ecs;
using StubbFramework.Scenes;
using StubbUnity.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Scenes
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        private Scene _scene;
        private ISceneName _sceneName;
        private ISceneContentController _content;
        private EcsEntity _entity = EcsEntity.Null;

        public bool IsDestroyed => _content == null;
        public ISceneName SceneName => _sceneName;
        public bool IsContentActive => _content.IsActive;
        public bool IsMain => SceneManager.GetActiveScene() == _scene;
        public bool HasEntity => _entity.IsAlive(); 

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _scene = gameObject.scene;
            _sceneName = new SceneName(_scene.name, _scene.path);
            _content = _scene.GetContentController<SceneContentController>();
        }

        public void SetAsMain()
        {
            SceneManager.SetActiveScene(_scene); 
        }

        public void ShowContent()
        {
            _content.Show();
        }

        public void HideContent()
        {
            _content.Hide();
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public void Destroy()
        {
            if (IsDestroyed == false)
            {
                _content.Destroy();
                _content = null;
            }
        }

        void OnDestroy()
        {
            if (HasEntity)
            {
                _entity.Destroy();
                _entity = EcsEntity.Null;
            }
            
            Destroy();
        }
    }
}