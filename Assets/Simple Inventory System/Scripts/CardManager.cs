using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedstoneinventeGameStudio
{
    public class CardManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
#nullable enable
        public InventoryItemData? itemData;
        public bool isOccupied;
#nullable disable

        [SerializeField] bool useAsDrag;
        [SerializeField] GameObject emptyCard;

        [SerializeField] TMP_Text itemName;
        [SerializeField] Image itemIcon;
        
        [SerializeField] CardsManager cardsManager;

        private void Awake()
        {
            if (useAsDrag)
            {
                InventoryManager.DragCard = this;
                isOccupied = true;

                gameObject.SetActive(false);
            }

            if (itemData == null)
            {
                RefreshDisplay();
            }
            else
            {
                SetItem(itemData);
            }
            
            if (cardsManager == null)
                cardsManager = GetComponentInParent<CardsManager>();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            if (useAsDrag || !isOccupied)
            {
                return;
            }

            InventoryManager.FromCard = this;
            TooltipManagerInventory.UnSetToolTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
            if (isOccupied)
            {
                InventoryManager.ToCard = InventoryManager.FromCard;

                if (InventoryManager.ToCard == default)
                {
                    TooltipManagerInventory.SetTooltip(itemData);
                }

                return;
            }

            InventoryManager.ToCard = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            if (!isOccupied)
            {
                return;
            }

            TooltipManagerInventory.UnSetToolTip();
        }

        public bool SetItem(InventoryItemData itemData)
        {
            if ((isOccupied && !useAsDrag) || itemData == null)
            {
                return false;
            }

            this.itemData = itemData;
            this.itemName.text = itemData.name;
            this.itemIcon.sprite = itemData.itemIcon;

            this.isOccupied = true;

            RefreshDisplay();

            return true;
        }

        public void UnSetItem()
        {
            itemData = null;
            this.isOccupied = false;

            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            emptyCard.SetActive(!isOccupied);
        }
    }

}