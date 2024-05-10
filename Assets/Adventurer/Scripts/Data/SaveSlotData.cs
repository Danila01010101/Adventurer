using UnityEngine;

namespace Adventurer
{
    public class SaveSlotData : MonoBehaviour
    {
        public int CurrentLevel { get; private set; }
        public CharacterType Character { get; private set; }
    }
}