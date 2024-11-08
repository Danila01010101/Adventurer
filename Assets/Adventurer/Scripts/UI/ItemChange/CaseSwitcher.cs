using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Adventurer
{
    public class CaseSwitcher : MonoBehaviour
    {
        private CaseBrain lastCase;

        public CaseBrain GetCaseUnderMouse()
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                CaseBrain caseButton = result.gameObject.GetComponent<CaseBrain>();
                if (caseButton != null)
                {
                    Debug.Log("Над кнопкой: " + caseButton.name);
                    return caseButton;
                }
            }

            return null;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CaseBrain caseUnderMouse = GetCaseUnderMouse();

                if (caseUnderMouse == null)
                    return;

                SelectCell(caseUnderMouse);
            }

            if (Input.GetMouseButtonUp(0))
            {
                CaseBrain caseUnderMouse = GetCaseUnderMouse();

                if (caseUnderMouse == null)
                {
                    lastCase = null;
                    return;
                }

                SelectCell(caseUnderMouse);
            }
        }

        private void SelectCell(CaseBrain cell)
        {
            if (lastCase == null)
            {
                lastCase = cell;
                cell.StartDrag();
            }
            else if (cell.CanPlace(lastCase.ItemData.ItemType) && cell != lastCase)
            {
                SwapCellsData(lastCase, cell);
            }
            else
            {
                lastCase.EndDrag();
                lastCase = null;
            }
        }

        private void SwapCellsData(CaseBrain firstCell, CaseBrain secondCell)
        {
            var secondCellItem = secondCell.ItemData;
            secondCell.SetItem(firstCell.ItemData);

            if (secondCellItem != null)
            {
                firstCell.SetItem(secondCellItem);
                firstCell.EndDrag();
            }
            else
            {
                firstCell.Reset();
            }

            lastCase = null;
        }
    }
}