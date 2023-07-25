using StubbUnity.StubbFramework.Logging;
using StubbUnity.Unity.Physics.Settings;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    [RequireComponent(typeof(Collider))]
    public class EcsCollisionSettings : MonoBehaviour
    {
        [SerializeField] private int typeId;
        [SerializeField] private CollisionDispatchProperties triggerProperties;
        [SerializeField] private CollisionDispatchProperties collisionProperties;

        public int TypeId => typeId;
        public CollisionDispatchingSettings DispatchingSettings { get; private set; }
        public Collider Collider { get; private set; }

        private void Awake()
        {
            DispatchingSettings = new CollisionDispatchingSettings(triggerProperties, collisionProperties, gameObject);
        }

        private void Start()
        {
            Collider = gameObject.GetComponent<Collider>();
            
            if (typeId <= 0)
                log.Warn($"{gameObject.name}. TypeId setting for the collision should be > 0.");
        }

        public void OnDestroy()
        {
            DispatchingSettings.Dispose();
            DispatchingSettings = null;
            Collider = null;
        }
    }
}