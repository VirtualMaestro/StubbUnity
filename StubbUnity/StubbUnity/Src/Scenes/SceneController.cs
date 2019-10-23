using Leopotam.Ecs;
using StubbFramework.Scenes;
using StubbUnity.Extensions;
using StubbUnity.Logging;
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
                log.Warn($"SceneController.Destroy. Controller with scene '{SceneName.FullName}' is already destroyed!");
                return;
            }

            _content.Hide();
            _content.Destroy();
            _content = null;
            SceneManager.UnloadSceneAsync(_scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        void OnDestroy()
        {
            _DestroyEntity();
            Destroy();
        }

        private void _DestroyEntity()
        {
            if (HasEntity)
            {
                _entity.Destroy();
                _entity = EcsEntity.Null;
            }
        }
    }
}