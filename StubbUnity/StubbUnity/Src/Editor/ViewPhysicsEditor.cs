using System.Runtime.CompilerServices;
using StubbUnity.Extensions;
using StubbUnity.Physics;
using StubbUnity.Physics.Collisions;
using StubbUnity.Physics.Settings;
using StubbUnity.Physics.Triggers;
using UnityEditor;
using UnityEngine;

namespace StubbUnity.Editor
{
    [CustomEditor(typeof(EcsViewPhysics))]
    public class ViewPhysicsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var viewPhysics = (EcsViewPhysics) target;
            var gameObject = viewPhysics.gameObject;
            ref var triggerSettings = ref viewPhysics.GetTriggerSettings();
            ref var collisionSettings = ref viewPhysics.GetCollisionSettings();

            _CheckTriggerSettings(ref triggerSettings, gameObject);
            _CheckCollisionSettings(ref collisionSettings, gameObject);
        }

        private void _CheckTriggerSettings(ref EditorCollisionDispatchSettings triggerSettings, GameObject gameObject)
        {
            _CheckTriggerEnter(triggerSettings.Enter, gameObject);
            _CheckTriggerEnter2D(triggerSettings.Enter2D, gameObject);

            _CheckTriggerStay(triggerSettings.Stay, gameObject);
            _CheckTriggerStay2D(triggerSettings.Stay2D, gameObject);

            _CheckTriggerExit(triggerSettings.Exit, gameObject);
            _CheckTriggerExit2D(triggerSettings.Exit2D, gameObject);
        }

        private void _CheckCollisionSettings(ref EditorCollisionDispatchSettings collisionSettings, GameObject gameObject)
        {
            _CheckCollisionEnter(collisionSettings.Enter, gameObject);
            _CheckCollisionEnter2D(collisionSettings.Enter2D, gameObject);

            _CheckCollisionStay(collisionSettings.Stay, gameObject);
            _CheckCollisionStay2D(collisionSettings.Stay2D, gameObject);

            _CheckCollisionExit(collisionSettings.Exit, gameObject);
            _CheckCollisionExit2D(collisionSettings.Exit2D, gameObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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