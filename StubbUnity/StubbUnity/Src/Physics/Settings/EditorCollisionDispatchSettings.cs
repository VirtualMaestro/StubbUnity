using System;

namespace StubbUnity.Physics.Settings
{
    [Serializable]
    public struct EditorCollisionDispatchSettings
    {
        public bool Enter;
        public bool Stay;
        public bool Exit;

        public bool Enter2D;
        public bool Stay2D;
        public bool Exit2D;
    }
}