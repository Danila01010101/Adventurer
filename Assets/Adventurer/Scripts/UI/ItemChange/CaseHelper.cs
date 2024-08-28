using System;
using System.Collections;
using System.Collections.Generic;
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
            else
            {
                SwapCellsData(lastCase, cell);
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
        private void OnEnable()
        {
            CaseBrain.CaseClicked += SelectCell;
        }

         private void OnDisable()
         {
         CaseBrain.CaseClicked -= SelectCell;
         }
        
    }

}
