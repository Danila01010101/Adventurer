using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventurer
{
    public class CaseHelper : MonoBehaviour
    {
        private CaseWeapon lastCase;

        private void Awake()
        {
            CaseWeapon.CaseClicked += SelectCell;
        }

        private void SelectCell(CaseWeapon cell)
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

        private void SwapCellsData(CaseWeapon firstCell, CaseWeapon secondCell)
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
        //private void OnEnable()
        //{
        //    CaseWeapon.CaseClicked += SelectCell;
        //}

        //private void OnDisable()
        //{
        //    CaseWeapon.CaseClicked -= SelectCell;
        //}
    }
}
