using StubbFramework.Scenes;
using UnityEngine;

namespace StubbUnity.Scenes
{
    public class SceneContentController : MonoBehaviour, ISceneContentController
    {
        public virtual bool IsActive => gameObject.activeSelf;

        public virtual void Show()
        {
            if (IsActive == false)
            {
                gameObject.SetActive(true);
            }
        }

        public virtual void Hide()
        {
            if (IsActive)
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void Destroy()
        {
            
        }
    }
}