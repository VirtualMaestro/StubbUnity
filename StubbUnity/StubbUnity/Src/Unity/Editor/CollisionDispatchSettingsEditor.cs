using StubbUnity.Unity.Extensions;
using StubbUnity.Unity.Physics.Dispatchers;
using StubbUnity.Unity.View;
using UnityEditor;
using UnityEngine;

namespace StubbUnity.Unity.Editor
{
    [CustomEditor(typeof(EcsViewLink))]
    public class CollisionDispatchSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var viewPhysics = (EcsViewLink) target;
            var gameObject = viewPhysics.gameObject;
            var triggerSettings = viewPhysics.GetTriggerProperties();
            var collisionSettings = viewPhysics.GetCollisionProperties();

            _CheckExistingDispatcherComponent<TriggerEnterDispatcher>(triggerSettings.Enter, gameObject);
            _CheckExistingDispatcherComponent<TriggerEnter2DDispatcher>(triggerSettings.Enter2D, gameObject);

            _CheckExistingDispatcherComponent<TriggerStayDispatcher>(triggerSettings.Stay, gameObject);
            _CheckExistingDispatcherComponent<TriggerStay2DDispatcher>(triggerSettings.Stay2D, gameObject);

            _CheckExistingDispatcherComponent<TriggerExitDispatcher>(triggerSettings.Exit, gameObject);
            _CheckExistingDispatcherComponent<TriggerExit2DDispatcher>(triggerSettings.Exit2D, gameObject);

            _CheckExistingDispatcherComponent<CollisionEnterDispatcher>(collisionSettings.Enter, gameObject);
            _CheckExistingDispatcherComponent<CollisionEnter2DDispatcher>(collisionSettings.Enter2D, gameObject);

            _CheckExistingDispatcherComponent<CollisionStayDispatcher>(collisionSettings.Stay, gameObject);
            _CheckExistingDispatcherComponent<CollisionStay2DDispatcher>(collisionSettings.Stay2D, gameObject);

            _CheckExistingDispatcherComponent<CollisionExitDispatcher>(collisionSettings.Exit, gameObject);
            _CheckExistingDispatcherComponent<CollisionExit2DDispatcher>(collisionSettings.Exit2D, gameObject);
        }

        private void _CheckExistingDispatcherComponent<T>(bool isEnabled, GameObject gameObject) where T : MonoBehaviour
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<T>())
                    gameObject.AddComponent<T>();
            }
            else if (gameObject.HasComponent<T>())
                DestroyImmediate(gameObject.GetComponent<T>());
        }
    }
}