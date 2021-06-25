using StubbUnity.StubbFramework.Pooling;
using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    public class CreatorGameObject : ICreator<GameObject>
    {
        private IPool<GameObject> _pool;
        private GameObject _prefab;
        private Quaternion _rotation;
        private Vector3 _scale;
        
        public CreatorGameObject(GameObject prefab, IPool<GameObject> pool)
        {
            _pool = pool;
            _prefab = Object.Instantiate(prefab);
            _prefab.AddComponent<PoolableMono>();
            _prefab.SetActive(false);
            _rotation = _prefab.transform.rotation;
            _scale = _prefab.transform.localScale;
            
            Object.DontDestroyOnLoad(_prefab);
        }

        public GameObject OnCreateInstance()
        {
            throw new System.NotImplementedException();
        }

        public void OnToPool(GameObject t)
        {
            throw new System.NotImplementedException();
        }

        public void OnFromPool(GameObject t)
        {
            throw new System.NotImplementedException();
        }

        public void OnDestroyInstance(GameObject instance)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Object.Destroy(_prefab);
            _prefab = default;
            _pool = null;
        }
    }
}