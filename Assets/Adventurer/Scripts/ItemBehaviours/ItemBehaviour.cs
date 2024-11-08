using UnityEngine;

namespace Adventurer
{
	public abstract class ItemBehaviour : MonoBehaviour
	{
		public abstract void ShowItem();
		public abstract void Use();
        public abstract void HideItem();
    }
}