using System;

namespace Adventurer
{

    [Serializable]
    public struct SaveSlotData
    {
        public int LastSceneIndex;
        //public SceneID LastSceneIndex;
        public Vector3 LastPosition;
        public Vector3 LastRotation;

        [Serializable]
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
        }
    }
}