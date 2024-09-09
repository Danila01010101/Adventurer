using System;
using UnityEngine;

namespace Adventurer
{
    public class CaseBrain : MonoBehaviour
    {
        [SerializeField] private ItemData item;
        [SerializeField] private int howManyItems;
        [SerializeField] private ItemType type;

        public ItemData ItemData { get { return item; } }
        public static Action<CaseBrain> CaseClicked;

        // public static Action<CaseBrain> ZeroingIsNeeded;

        private Vector2 StartPosition;

        public bool CanPlace(ItemType newItemType)
        {
            if (newItemType == type)
            {
                return true;
            }

            return false;
        }

        private void Awake()
        {
            if (item != null)
            {
               gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = item.Icon;
            }
        }

        public void SetItem(ItemData item)
        {
            if (item == null)
            {
                throw new ArgumentException("Can't set empty item");
            }

            if (item.ItemType != type)
            {
                throw new ArgumentException($"Can't place {item.ItemType} in this cell");
            }

            this.item = item;
            gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = item.Icon;
            EndDrag();
        }

        public void Reset() 
        {
            item = null;
            gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = null;
            EndDrag();
        }

        public void StartDrag() => gameObject.transform.GetChild(0).gameObject.SetActive(true);

        public void EndDrag() => gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }   
}