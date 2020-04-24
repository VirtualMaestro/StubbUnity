using System;
using Leopotam.Ecs;
using StubbFramework;
using StubbFramework.Common.Names;
using StubbFramework.Logging;
using StubbFramework.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Scenes
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        [SerializeField] private GameObject content;
        private Scene _scene;
        private EcsEntity _entity = EcsEntity.Null;

        public IAssetName SceneName { get; private set; }
        public bool IsContentActive => content.activeSelf;
        public bool IsMain => SceneManager.GetActiveScene() == _scene;
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public bool IsDisposed { get; private set; }
        public EcsWorld World { get; private set; }

        private void Start()
        {
            IsDisposed = false;
            World = Stubb.World;
            _scene = gameObject.scene;
            SceneName = new SceneName(_scene.name, _scene.path);

            if (content == null)
                throw new Exception($"Content wasn't set for the controller of the scene '{SceneName}'!");

            Initialize();
        }

        /// <summary>
        /// Init user's code here.
        /// </summary>
        public virtual void Initialize()
        {
        }

        public void SetAsMain()
        {
            SceneManager.SetActiveScene(_scene);
        }

        public void ShowContent()
        {
            if (!IsDisposed && IsContentActive == false)
            {
                content.SetActive(true);
            }
        }

        public void HideContent()
        {
            if (!IsDisposed && IsContentActive)
            {
                content.SetActive(false);
            }
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                log.Warn(
                    $"SceneController.Destroy. Controller with scene '{SceneName.FullName}' is already destroyed!");
                return;
            }

            IsDisposed = true;

            if (HasEntity) _entity.Destroy();
            _entity = EcsEntity.Null;

            SceneManager.UnloadSceneAsync(_scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        private void OnDestroy()
        {
            if (IsDisposed) return;
            
            Dispose();
        }
    }
}