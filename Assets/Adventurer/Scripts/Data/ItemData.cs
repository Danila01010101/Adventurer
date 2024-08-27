using UnityEngine;

namespace Adventurer
{
	[CreateAssetMenu(fileName = "Item", menuName = "Items/NewItem")]
    public class ItemData : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; }
		[field:SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject ItemModel { get; private set; }
        [field: SerializeField]  public ItemType ItemType { get; private set; }
    }
}