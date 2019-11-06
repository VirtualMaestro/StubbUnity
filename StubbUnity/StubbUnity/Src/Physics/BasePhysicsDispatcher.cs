using Leopotam.Ecs;
using StubbFramework;
using UnityEngine;

namespace StubbUnity.Physics
{
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected EcsWorld World;
        
        private void Start()
        {
            World = Stubb.GetContext().World;
        }
    }
}