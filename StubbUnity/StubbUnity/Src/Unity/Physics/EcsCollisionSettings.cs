using StubbUnity.StubbFramework.Logging;
using StubbUnity.Unity.Physics.Settings;
using StubbUnity.Unity.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    [RequireComponent(typeof(Collider))]
    public class EcsCollisionSettings : MonoBehaviour
    {
        [SerializeField] private int typeId;
        [SerializeField] private EcsViewLink attachedView;
        [SerializeField] private CollisionDispatchProperties triggerProperties;
        [SerializeField] private CollisionDispatchProperties collisionProperties;

        public int TypeId => typeId;
        public CollisionDispatchingSettings DispatchingSettings { get; private set; }
        public Collider Collider { get; private set; }

        public EcsViewLink View
        {
            get
            {
                if (attachedView != null) 
                    return attachedView;
                
                if (gameObject.TryGetComponent<EcsViewLink>(out var view))
                    attachedView = view;
                else  // find in parent
                {
                    log.Warn($"{nameof(EcsCollisionSettings)} missing direct reference to attachView. Used GetComponentInParent, that might influence performance!");
                    attachedView = gameObject.GetComponentInParent<EcsViewLink>();

                    if (attachedView == null)
                        log.Warn($"{nameof(EcsCollisionSettings)} attachedView wasn't found! Use direct reference!");
                }

                return attachedView;
            }
            
            private set => attachedView = value;
        }

        private void Awake()
        {
            DispatchingSettings = new CollisionDispatchingSettings(triggerProperties, collisionProperties, gameObject);
        }

        private void Start()
        {
            Collider = gameObject.GetComponent<Collider>();
            View = attachedView;
            
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