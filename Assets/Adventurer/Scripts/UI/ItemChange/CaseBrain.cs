using System;
using UnityEngine;

namespace Adventurer
{
    [RequireComponent(typeof(ItemCaseView))]
    public class CaseBrain : MonoBehaviour, IItemCase
    {
        [SerializeField] private ItemData item;
        [SerializeField] private ItemType type;

        public ItemData ItemData { get { return item; } }
        public Action ItemChanged { get; set; }
        public static Action<CaseBrain> CaseClicked;

        private ItemCaseView caseView;
        private Vector2 StartPosition;

        private void Start()
        {
            caseView = GetComponent<ItemCaseView>();
            caseView.Initialize(this);
        }

        public bool CanPlace(ItemType newItemType)
        {
            if (newItemType == type)
            {
                return true;
            }

            return false;
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
            ItemChanged?.Invoke();
            EndDrag();
        }

        public void Reset() 
        {
            item = null;
            ItemChanged?.Invoke();
            EndDrag();
        }

        public void StartDrag() => gameObject.transform.GetChild(0).gameObject.SetActive(true);

        public void EndDrag() => gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }   
}