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
            // Создаем список для хранения результатов рэйкаста
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            // Создаем данные указателя мыши
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;  // Текущая позиция мыши

            // Выполняем рэйкаст
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            // Проходимся по результатам рэйкаста
            foreach (RaycastResult result in raycastResults)
            {
                // Проверяем, есть ли у объекта компонент CaseBrain
                CaseBrain caseButton = result.gameObject.GetComponent<CaseBrain>();
                if (caseButton != null)
                {
                    Debug.Log("Над кнопкой: " + caseButton.name);
                    return caseButton;  // Возвращаем найденную кнопку
                }
            }

            // Если кнопка не найдена, возвращаем null
            return null;
        }

        void Update()
        {
            // Проверяем в каждом кадре, есть ли кнопка под мышью

            if (Input.GetMouseButtonDown(0))
            {
                CaseBrain caseUnderMouse = GetCaseUnderMouse();

                if (caseUnderMouse == null)
                    return;

                // Логика для работы с кнопкой, начало движения
                SelectCell(caseUnderMouse);
            }

            if (Input.GetMouseButtonUp(0))
            {
                CaseBrain caseUnderMouse = GetCaseUnderMouse();

                if (caseUnderMouse == null)
                    return;

                // Логика для работы с кнопкой, конец движения
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