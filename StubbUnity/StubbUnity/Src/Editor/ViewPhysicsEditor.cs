using StubbUnity.Extensions;
using StubbUnity.Physics;
using StubbUnity.Physics.Collisions;
using StubbUnity.Physics.Settings;
using StubbUnity.Physics.Triggers;
using UnityEditor;
using UnityEngine;

namespace StubbUnity.Editor
{
    [CustomEditor(typeof(ViewPhysics))]
    public class ViewPhysicsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var viewPhysics = (ViewPhysics) target;
            var gameObject = viewPhysics.gameObject;
            ref var triggerSettings = ref viewPhysics.GetTriggerSettings();
            ref var collisionSettings = ref viewPhysics.GetCollisionSettings();

            _CheckTriggerSettings(ref triggerSettings, gameObject);
            _CheckCollisionSettings(ref collisionSettings, gameObject);
        }

        private void _CheckTriggerSettings(ref EditorCollisionDispatchSettings triggerSettings, GameObject gameObject)
        {
            if (triggerSettings.Enter)
            {
                if (!gameObject.HasComponent<TriggerEnterDispatcher>())
                {
                    gameObject.AddComponent<TriggerEnterDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<TriggerEnterDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<TriggerEnterDispatcher>());
                }
            }
            
            if (triggerSettings.Stay)
            {
                if (!gameObject.HasComponent<TriggerStayDispatcher>())
                {
                    gameObject.AddComponent<TriggerStayDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<TriggerStayDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<TriggerStayDispatcher>());
                }
            }
            
            if (triggerSettings.Exit)
            {
                if (!gameObject.HasComponent<TriggerExitDispatcher>())
                {
                    gameObject.AddComponent<TriggerExitDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<TriggerExitDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<TriggerExitDispatcher>());
                }
            }
        }

        private void _CheckCollisionSettings(ref EditorCollisionDispatchSettings collisionSettings, GameObject gameObject)
        {
            if (collisionSettings.Enter)
            {
                if (!gameObject.HasComponent<CollisionEnterDispatcher>())
                {
                    gameObject.AddComponent<CollisionEnterDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<CollisionEnterDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<CollisionEnterDispatcher>());
                }
            }
            
            if (collisionSettings.Stay)
            {
                if (!gameObject.HasComponent<CollisionStayDispatcher>())
                {
                    gameObject.AddComponent<CollisionStayDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<CollisionStayDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<CollisionStayDispatcher>());
                }
            }
            
            if (collisionSettings.Exit)
            {
                if (!gameObject.HasComponent<CollisionExitDispatcher>())
                {
                    gameObject.AddComponent<CollisionExitDispatcher>();
                }
            }
            else
            {
                if (gameObject.HasComponent<CollisionExitDispatcher>())
                {
                    DestroyImmediate(gameObject.GetComponent<CollisionExitDispatcher>());
                }
            }
        }
    }
}