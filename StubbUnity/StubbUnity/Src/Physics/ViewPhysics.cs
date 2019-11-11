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
        private bool dynamicSetup;
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

        public int TypeId { get; set; }

        public bool EnableTriggerEnter
        {
            get => _triggerEnter != null;
            set
            {
                if ((value == false && _triggerEnter == null) || (value && _triggerEnter != null)) return;

                if (value)
                {
                   _triggerEnter =  gameObject.AddComponent<TriggerEnterDispatcher>();
                }
                else
                {
                    Destroy(_triggerEnter);
                    _triggerEnter = null;
                }
            }
        }
        
        public bool EnableTriggerStay
        {
            get => _triggerStay != null;
            set
            {
                if ((value == false && _triggerStay == null) || (value && _triggerStay != null)) return;

                if (value)
                {
                    _triggerStay =  gameObject.AddComponent<TriggerStayDispatcher>();
                }
                else
                {
                    Destroy(_triggerStay);
                    _triggerStay = null;
                }
            }
        }
        
        public bool EnableTriggerExit
        {
            get => _triggerExit != null;
            set
            {
                if ((value == false && _triggerExit == null) || (value && _triggerExit != null)) return;

                if (value)
                {
                    _triggerExit =  gameObject.AddComponent<TriggerExitDispatcher>();
                }
                else
                {
                    Destroy(_triggerExit);
                    _triggerExit = null;
                }
            }
        }
        
        public bool EnableCollisionEnter 
        {       
            get => _collisionEnter != null;
            set
            {
                if ((value == false && _collisionEnter == null) || (value && _collisionEnter != null)) return;

                if (value)
                {
                    _collisionEnter =  gameObject.AddComponent<CollisionEnterDispatcher>();
                }
                else
                {
                    Destroy(_collisionEnter);
                    _collisionEnter = null;
                }
            }
        }

        public bool EnableCollisionStay
        {
            get => _collisionStay != null;
            set
            {
                if ((value == false && _collisionStay == null) || (value && _collisionStay != null)) return;

                if (value)
                {
                    _collisionStay =  gameObject.AddComponent<CollisionStayDispatcher>();
                }
                else
                {
                    Destroy(_collisionStay);
                    _collisionStay = null;
                }
            }
        }

        public bool EnableCollisionExit
        {
            get => _collisionExit != null;
            set
            {
                if ((value == false && _collisionExit == null) || (value && _collisionExit != null)) return;

                if (value)
                {
                    _collisionExit =  gameObject.AddComponent<CollisionExitDispatcher>();
                }
                else
                {
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

        void Start()
        {
            if (dynamicSetup)
            {
                EnableTriggerEnter = triggerSettings.Enter;
                EnableTriggerStay = triggerSettings.Stay;
                EnableTriggerExit = triggerSettings.Exit;
            
                EnableCollisionEnter = collisionSettings.Enter;
                EnableCollisionStay = collisionSettings.Stay;
                EnableCollisionExit = collisionSettings.Exit;
            }
            else
            {
                _triggerEnter = gameObject.GetComponent<TriggerEnterDispatcher>();
                _triggerStay = gameObject.GetComponent<TriggerStayDispatcher>();
                _triggerExit = gameObject.GetComponent<TriggerExitDispatcher>();

                _collisionEnter = gameObject.GetComponent<CollisionEnterDispatcher>();
                _collisionStay = gameObject.GetComponent<CollisionStayDispatcher>();
                _collisionExit = gameObject.GetComponent<CollisionExitDispatcher>();
            }
        }
    }
}