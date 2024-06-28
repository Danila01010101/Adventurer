using UnityEngine;

namespace Adventurer
{
    public class SaveSlotData : MonoBehaviour
    {
        public int LastSceneIndex { get; private set; }
        public Vector3 LastPosition { get; private set; }
        public Vector3 LastRotation { get; private set; }
    }
}