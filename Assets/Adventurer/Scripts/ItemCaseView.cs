using UnityEngine;
using UnityEngine.UI;

namespace Adventurer
{
    public class ItemCaseView : MonoBehaviour
	{
		[SerializeField] private Image icon;

        private ItemData itemData => itemCase.ItemData;

        private IItemCase itemCase;

    	public void Initialize(IItemCase itemCase)
		{
			this.itemCase = itemCase;
            itemCase.ItemChanged += UpdateView;
			UpdateView();
		}

        private void UpdateView()
		{
            if (itemData != null)
            {
                icon.sprite = itemData.Icon;
            }
			else
			{
                icon.sprite = null;
            }
        }
	}
}