using StubbFramework.Physics;
using StubbUnity.Physics.Settings;
using StubbUnity.Physics.Triggers;
using StubbUnity.View;
using UnityEngine;

namespace StubbUnity.Physics
{
    public class ViewPhysics : ViewObject, IViewPhysics
    {
        [SerializeField]
        private EditorCollisionDispatchSettings _triggerSettings;
        [SerializeField]
        private EditorCollisionDispatchSettings _collisionSettings;
        
        private TriggerEnterDispatcher _triggerEnter;
        private TriggerStayDispatcher _triggerStay;
        private TriggerExitDispatcher _triggerExit;

        public int TypeId { get; set; }

        public bool EnableTriggerEnter
        {
            get => _triggerEnter != null;
            set
            {
                if ((value == false && _triggerEnter == null) || (value == true && _triggerEnter != null)) return;

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
                if ((value == false && _triggerStay == null) || (value == true && _triggerStay != null)) return;

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
                if ((value == false && _triggerExit == null) || (value == true && _triggerExit != null)) return;

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
        
        
        public bool EnableCollisionEnter { get; set; }
        public bool EnableCollisionStay { get; set; }
        public bool EnableCollisionExit { get; set; }
    }
}