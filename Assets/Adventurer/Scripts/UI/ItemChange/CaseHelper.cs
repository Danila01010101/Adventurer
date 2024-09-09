using UnityEngine;

namespace Adventurer
{
    public class CaseHelper : MonoBehaviour
    {
        private CaseBrain lastCase;
        private void SelectCell(CaseBrain cell)
        {

            if (lastCase == null)
            {
                lastCase = cell;
            }
            else if (cell.CanPlace(lastCase.ItemData.ItemType))
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
            }
            else
            {
                firstCell.Reset();
            }

            lastCase = null;
        }

         private void Zeroing(CaseBrain G)
        {
            lastCase = null;
        }

        private void OnEnable()
        {
            CaseBrain.CaseClicked += SelectCell;
           // CaseBrain.ZeroingIsNeeded += Zeroing;
        }

        private void OnDisable()
        {
            CaseBrain.CaseClicked -= SelectCell;
           // CaseBrain.ZeroingIsNeeded -= Zeroing;
        }
    }
}