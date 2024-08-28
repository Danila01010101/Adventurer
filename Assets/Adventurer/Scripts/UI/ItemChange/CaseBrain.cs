using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Adventurer
{
    public class CaseBrain : MonoBehaviour
    {
        [SerializeField] private ItemData item;
        [SerializeField] private int howManyItems;
        [SerializeField] private ItemType type;

        public ItemData ItemData { get { return item; } }
        public static Action<CaseBrain> CaseClicked;

        private bool Crutch;
    


        private void Awake()
        {
            if (item != null)
            {
               gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = item.Icon;
            }
        }

        private void Update()
        {
          
        }

        private void OnMouseUp()
        {
           CaseClicked?.Invoke(this);
        }

        private void OnMouseDown()
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            CaseClicked?.Invoke(this);
            Crutch = true;
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
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Reset() 
        {
            item = null;
            gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = null;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }   
}
