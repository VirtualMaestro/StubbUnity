using StubbFramework.Physics;
using StubbUnity.Physics.Collisions;
using StubbUnity.Physics.Settings;
using StubbUnity.Physics.Triggers;
using StubbUnity.View;
using UnityEngine;

namespace StubbUnity.Physics
{
    public sealed class ViewPhysics : ViewObject, IViewPhysics
    {
        [SerializeField]
        private EditorCollisionDispatchSettings triggerSettings;
        [SerializeField]
        private EditorCollisionDispatchSettings collisionSettings;
        
        private TriggerEnterDispatcher _triggerEnter;
        private TriggerStayDispatcher _triggerStay;
        private TriggerExitDispatcher _triggerExit;

        private CollisionEnterDispatcher _collisionEnter;
        private CollisionStayDispatcher _collisionStay;
        private CollisionExitDispatcher _collisionExit;

        /// <summary>
        /// int number which represents type for an object.
        /// This type will be used for determination which object it is and for setting up collision pair.
        /// It determines if collision event will be sent during a collision of two objects.
        /// Default value 0, which means no collision events will be sent.
        /// </summary>
        public int TypeId { get; set; }

        public bool EnableTriggerEnter
        {
            get => triggerSettings.Enter;
            set
            {
                if (value && !triggerSettings.Enter)
                {
                    triggerSettings.Enter = true;
                    _triggerEnter = gameObject.AddComponent<TriggerEnterDispatcher>();
                } 
                else if (!value && triggerSettings.Enter)
                {
                    triggerSettings.Enter = false;
                    Destroy(_triggerEnter);
                    _triggerEnter = null;
                }
            }
        }
        
        public bool EnableTriggerStay
        {
            get => triggerSettings.Stay;
            set
            {
                if (value && !triggerSettings.Stay)
                {
                    triggerSettings.Stay = true;
                    _triggerStay = gameObject.AddComponent<TriggerStayDispatcher>();
                } 
                else if (!value && triggerSettings.Stay)
                {
                    triggerSettings.Stay = false;
                    Destroy(_triggerStay);
                    _triggerStay = null;
                }
            }
        }
        
        public bool EnableTriggerExit
        {
            get => triggerSettings.Exit;
            set
            {
                if (value && !triggerSettings.Exit)
                {
                    triggerSettings.Exit = true;
                    _triggerExit = gameObject.AddComponent<TriggerExitDispatcher>();
                } 
                else if (!value && triggerSettings.Exit)
                {
                    triggerSettings.Exit = false;
                    Destroy(_triggerExit);
                    _triggerExit = null;
                }
            }
        }
        
        public bool EnableCollisionEnter 
        {       
            get => collisionSettings.Enter;
            set
            {
                if (value && !collisionSettings.Enter)
                {
                    collisionSettings.Enter = true;
                    _collisionEnter = gameObject.AddComponent<CollisionEnterDispatcher>();
                } 
                else if (!value && collisionSettings.Enter)
                {
                    collisionSettings.Enter = false;
                    Destroy(_collisionEnter);
                    _collisionEnter = null;
                }
            }
        }

        public bool EnableCollisionStay
        {
            get => collisionSettings.Stay;
            set
            {
                if (value && !collisionSettings.Stay)
                {
                    collisionSettings.Stay = true;
                    _collisionStay = gameObject.AddComponent<CollisionStayDispatcher>();
                } 
                else if (!value && collisionSettings.Stay)
                {
                    collisionSettings.Stay = false;
                    Destroy(_collisionStay);
                    _collisionStay = null;
                }
            }
        }

        public bool EnableCollisionExit
        {
            get => collisionSettings.Exit;
            set
            {
                if (value && !collisionSettings.Exit)
                {
                    collisionSettings.Exit = true;
                    _collisionExit = gameObject.AddComponent<CollisionExitDispatcher>();
                } 
                else if (!value && collisionSettings.Exit)
                {
                    collisionSettings.Exit = false;
                    Destroy(_collisionExit);
                    _collisionExit = null;
                }
            }
        }
        
        public ref EditorCollisionDispatchSettings GetTriggerSettings()
        {
            return ref triggerSettings;
        }

        public ref EditorCollisionDispatchSettings GetCollisionSettings()
        {
            return ref collisionSettings;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            EnableTriggerEnter = false;
            EnableTriggerStay = false;
            EnableTriggerExit = false;

            EnableCollisionEnter = false;
            EnableCollisionStay = false;
            EnableCollisionExit = false;
        }
    }
}