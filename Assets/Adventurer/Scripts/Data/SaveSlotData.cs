using UnityEngine;

namespace Adventurer
{
    public class SaveSlotData
    {
        public SceneID LastSceneIndex { get; private set; }
        public Vector3 LastPosition { get; private set; }
        public Vector3 LastRotation { get; private set; }
    }
}