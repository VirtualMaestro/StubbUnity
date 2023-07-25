using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    [RequireComponent(typeof(EcsCollisionSettings))]
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected EcsWorld World { get; private set; }
        protected EcsCollisionSettings Settings { get; private set; }

        private void Awake()
        {
            World = Stubb.World;
        }

        private void Start()
        {
            Settings = gameObject.GetComponent<EcsCollisionSettings>();
        }
    }
}