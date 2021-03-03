using StubbUnity.Unity.Extensions;
using StubbUnity.Unity.Physics.Collisions;
using StubbUnity.Unity.Physics.Settings;
using StubbUnity.Unity.Physics.Triggers;
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

            _CheckTriggerSettings(triggerSettings, gameObject);
            _CheckCollisionSettings(collisionSettings, gameObject);
        }

        private void _CheckTriggerSettings(CollisionDispatchProperties triggerProperties, GameObject gameObject)
        {
            _CheckTriggerEnter(triggerProperties.Enter, gameObject);
            _CheckTriggerEnter2D(triggerProperties.Enter2D, gameObject);

            _CheckTriggerStay(triggerProperties.Stay, gameObject);
            _CheckTriggerStay2D(triggerProperties.Stay2D, gameObject);

            _CheckTriggerExit(triggerProperties.Exit, gameObject);
            _CheckTriggerExit2D(triggerProperties.Exit2D, gameObject);
        }

        private void _CheckCollisionSettings(CollisionDispatchProperties collisionProperties, GameObject gameObject)
        {
            _CheckCollisionEnter(collisionProperties.Enter, gameObject);
            _CheckCollisionEnter2D(collisionProperties.Enter2D, gameObject);

            _CheckCollisionStay(collisionProperties.Stay, gameObject);
            _CheckCollisionStay2D(collisionProperties.Stay2D, gameObject);

            _CheckCollisionExit(collisionProperties.Exit, gameObject);
            _CheckCollisionExit2D(collisionProperties.Exit2D, gameObject);
        }

        private void _CheckTriggerEnter(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerEnterDispatcher>())
                {
                    gameObject.AddComponent<TriggerEnterDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerEnterDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerEnterDispatcher>());
            }
        }

        private void _CheckTriggerEnter2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerEnter2DDispatcher>())
                {
                    gameObject.AddComponent<TriggerEnter2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerEnter2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerEnter2DDispatcher>());
            }
        }

        private void _CheckTriggerStay(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerStayDispatcher>())
                {
                    gameObject.AddComponent<TriggerStayDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerStayDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerStayDispatcher>());
            }
        }

        private void _CheckTriggerStay2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerStay2DDispatcher>())
                {
                    gameObject.AddComponent<TriggerStay2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerStay2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerStay2DDispatcher>());
            }
        }

        private void _CheckTriggerExit(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerExitDispatcher>())
                {
                    gameObject.AddComponent<TriggerExitDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerExitDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerExitDispatcher>());
            }
        }

        private void _CheckTriggerExit2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<TriggerExit2DDispatcher>())
                {
                    gameObject.AddComponent<TriggerExit2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<TriggerExit2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<TriggerExit2DDispatcher>());
            }
        }

        private void _CheckCollisionEnter(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionEnterDispatcher>())
                {
                    gameObject.AddComponent<CollisionEnterDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionEnterDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionEnterDispatcher>());
            }
        }

        private void _CheckCollisionEnter2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionEnter2DDispatcher>())
                {
                    gameObject.AddComponent<CollisionEnter2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionEnter2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionEnter2DDispatcher>());
            }
        }

        private void _CheckCollisionStay(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionStayDispatcher>())
                {
                    gameObject.AddComponent<CollisionStayDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionStayDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionStayDispatcher>());
            }
        }

        private void _CheckCollisionStay2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionStay2DDispatcher>())
                {
                    gameObject.AddComponent<CollisionStay2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionStay2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionStay2DDispatcher>());
            }
        }

        private void _CheckCollisionExit(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionExitDispatcher>())
                {
                    gameObject.AddComponent<CollisionExitDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionExitDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionExitDispatcher>());
            }
        }

        private void _CheckCollisionExit2D(bool isEnabled, GameObject gameObject)
        {
            if (isEnabled)
            {
                if (!gameObject.HasComponent<CollisionExit2DDispatcher>())
                {
                    gameObject.AddComponent<CollisionExit2DDispatcher>();
                }
            }
            else if (gameObject.HasComponent<CollisionExit2DDispatcher>())
            {
                DestroyImmediate(gameObject.GetComponent<CollisionExit2DDispatcher>());
            }
        }
    }
}