using System;

namespace Adventurer
{

    [Serializable]
    public struct SaveSlotData
    {
        public SceneID LastSceneIndex;
        public Vector3 LastPosition;
        public Vector3 LastRotation;

        public enum Slot { None = 0, First = 1, Second = 2, Third = 3, Fourth = 4 }

        [Serializable]
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
        }

    }
}