﻿using System;
using System.Runtime.CompilerServices;
using StubbFramework;
using StubbFramework.Common;
using StubbFramework.Scenes;
using StubbFramework.Scenes.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        private ISceneContentController _content;
        private Scene _scene;

        public bool IsDestroyed => _content == null;
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

            Stubb.World.NewEntityWith<SceneComponent, NewEntityComponent>(out var sceneComponent, out var newEntityComponent);
            sceneComponent.Scene = this;
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
            if (IsDestroyed == false)
            {
                _content.Destroy();
                _content = null;
            }
        }

        private void OnDestroy()
        {
            Destroy();
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