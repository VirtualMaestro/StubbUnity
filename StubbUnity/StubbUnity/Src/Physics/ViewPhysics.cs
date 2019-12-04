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
        private TriggerEnter2DDispatcher _triggerEnter2D;
        private TriggerStayDispatcher _triggerStay;
        private TriggerStay2DDispatcher _triggerStay2D;
        private TriggerExitDispatcher _triggerExit;
        private TriggerExit2DDispatcher _triggerExit2D;

        private CollisionEnterDispatcher _collisionEnter;
        private CollisionEnter2DDispatcher _collisionEnter2D;
        private CollisionStayDispatcher _collisionStay;
        private CollisionStay2DDispatcher _collisionStay2D;
        private CollisionExitDispatcher _collisionExit;
        private CollisionExit2DDispatcher _collisionExit2D;

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
        
        public bool EnableTriggerEnter2D
        {
            get => triggerSettings.Enter2D;
            set
            {
                if (value && !triggerSettings.Enter2D)
                {
                    triggerSettings.Enter2D = true;
                    _triggerEnter2D = gameObject.AddComponent<TriggerEnter2DDispatcher>();
                } 
                else if (!value && triggerSettings.Enter2D)
                {
                    triggerSettings.Enter2D = false;
                    Destroy(_triggerEnter2D);
                    _triggerEnter2D = null;
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
        
        public bool EnableTriggerStay2D
        {
            get => triggerSettings.Stay2D;
            set
            {
                if (value && !triggerSettings.Stay2D)
                {
                    triggerSettings.Stay2D = true;
                    _triggerStay2D = gameObject.AddComponent<TriggerStay2DDispatcher>();
                } 
                else if (!value && triggerSettings.Stay2D)
                {
                    triggerSettings.Stay2D = false;
                    Destroy(_triggerStay2D);
                    _triggerStay2D = null;
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
        
        public bool EnableTriggerExit2D
        {
            get => triggerSettings.Exit2D;
            set
            {
                if (value && !triggerSettings.Exit2D)
                {
                    triggerSettings.Exit2D = true;
                    _triggerExit2D = gameObject.AddComponent<TriggerExit2DDispatcher>();
                } 
                else if (!value && triggerSettings.Exit2D)
                {
                    triggerSettings.Exit2D = false;
                    Destroy(_triggerExit2D);
                    _triggerExit2D = null;
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

        public bool EnableCollisionEnter2D 
        {       
            get => collisionSettings.Enter2D;
            set
            {
                if (value && !collisionSettings.Enter2D)
                {
                    collisionSettings.Enter2D = true;
                    _collisionEnter2D = gameObject.AddComponent<CollisionEnter2DDispatcher>();
                } 
                else if (!value && collisionSettings.Enter2D)
                {
                    collisionSettings.Enter2D = false;
                    Destroy(_collisionEnter2D);
                    _collisionEnter2D = null;
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

        public bool EnableCollisionStay2D
        {
            get => collisionSettings.Stay2D;
            set
            {
                if (value && !collisionSettings.Stay2D)
                {
                    collisionSettings.Stay2D = true;
                    _collisionStay2D = gameObject.AddComponent<CollisionStay2DDispatcher>();
                } 
                else if (!value && collisionSettings.Stay2D)
                {
                    collisionSettings.Stay2D = false;
                    Destroy(_collisionStay2D);
                    _collisionStay2D = null;
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
        
        public bool EnableCollisionExit2D
        {
            get => collisionSettings.Exit2D;
            set
            {
                if (value && !collisionSettings.Exit2D)
                {
                    collisionSettings.Exit2D = true;
                    _collisionExit2D = gameObject.AddComponent<CollisionExit2DDispatcher>();
                } 
                else if (!value && collisionSettings.Exit2D)
                {
                    collisionSettings.Exit2D = false;
                    Destroy(_collisionExit2D);
                    _collisionExit2D = null;
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
            EnableTriggerEnter2D = false;
            EnableTriggerStay = false;
            EnableTriggerStay2D = false;
            EnableTriggerExit = false;
            EnableTriggerExit2D = false;

            EnableCollisionEnter = false;
            EnableCollisionEnter2D = false;
            EnableCollisionStay = false;
            EnableCollisionStay2D = false;
            EnableCollisionExit = false;
            EnableCollisionExit2D = false;
        }
    }
}