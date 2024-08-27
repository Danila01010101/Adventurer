using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Adventurer
{
    public class CaseWeapon : MonoBehaviour
    {
        [SerializeField] private ItemData item;
        [SerializeField] private int howManyItems;
        [SerializeField] private int IDCase = 0;
        [SerializeField] private int bagroudActive;
        [SerializeField] private ItemType type;

        public ItemData ItemData { get { return item; } }
        public static Action<CaseWeapon> CaseClicked;

 
        private void Awake()
        {
            if (item != null)
            {
                gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = item.Icon;
            }
            PlayerPrefs.SetInt("LastLD1", IDCase);

        }

        private void OnMouseDown()
        {
            CaseClicked?.Invoke(this);
            //IdMadgic();

        }

        private void IdMadgic()
        {
            GameObject[] cases;
            cases = GameObject.FindGameObjectsWithTag("InventaryPart");
            IDCase = PlayerPrefs.GetInt("LastLD1");
            cases[IDCase].transform.GetChild(0).gameObject.SetActive(false);
            
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                IDCase = cases.IndexOf(gameObject);
                PlayerPrefs.SetInt("LastLD1", IDCase);
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
        }

        public void Reset() 
        {
            item = null;
            gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = null;
        }
    }   
}
