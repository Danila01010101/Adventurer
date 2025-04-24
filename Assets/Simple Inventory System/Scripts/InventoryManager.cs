using UnityEngine;

namespace RedstoneinventeGameStudio
{
    public class InventoryManager : MonoBehaviour
    {
        public static CardManager DragCard;

        public static CardManager FromCard;
        public static CardManager ToCard;

        [SerializeField] Vector3 tooltipOffset;
        [SerializeField] Vector3 draggingCardOffset;
        [SerializeField] Transform playerInventory;
        [SerializeField] CardsManager cardsManager;
        
        [SerializeField] InventoryItemData testItemData;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && FromCard != default)
            {
                if (ToCard != default)
                {
                    ToCard.SetItem(DragCard.itemData);
                }
                else if (FromCard != default)
                {
                    FromCard.SetItem(DragCard.itemData);
                }

                ToCard = default;
                FromCard = default;

                DragCard.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && FromCard != default)
            {
                Debug.Log($"Dragging Card: {DragCard.itemData}");
                DragCard.SetItem(FromCard.itemData);
                FromCard.UnSetItem();

                DragCard.gameObject.SetActive(true);
            }

            DragCard.transform.position = Input.mousePosition + draggingCardOffset;
            TooltipManagerInventory.instance.transform.position = Input.mousePosition + tooltipOffset;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddToInventory(testItemData);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                RemoveFromInventory(cardsManager.items.IndexOf(testItemData));
            }
        }

        public void AddToInventory(InventoryItemData item)
        {
            if (cardsManager.emptyCards.Count > 0 && cardsManager.emptyCards[0] != null)
            {
                cardsManager.emptyCards[0].itemData = item;
                cardsManager.UpdateInventory();
                cardsManager.emptyCards[0].RefreshDisplay();
                Debug.Log($"Добавлен предмет: {item.itemName}");
            }
            else
            {
                Debug.LogWarning($"Инвентарь заполнен");
            }
        }

        public void RemoveFromInventory(int itemIndex)
        {
            if (cardsManager.fullCards.Count > 0 && cardsManager.fullCards[itemIndex] != null)
            {
                Debug.Log($"Убран предмет предмет: {cardsManager.fullCards[itemIndex]}");
                cardsManager.fullCards[itemIndex].itemData = null;
                cardsManager.UpdateInventory();
                cardsManager.emptyCards[0].RefreshDisplay();
                
            }
            else
            {
                Debug.LogError($"Ошибка");
            }
        }
    }

}