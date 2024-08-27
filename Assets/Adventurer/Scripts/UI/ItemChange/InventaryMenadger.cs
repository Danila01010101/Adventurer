using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventurer
{
    public class InventaryMenadger : MonoBehaviour
    {

        [SerializeField] private GameObject Inventary;
        [SerializeField] private GameObject Hotbar;
        [SerializeField] private GameObject Weaponlist;
        [SerializeField] private GameObject Skilllist;
        [SerializeField] private GameObject UsebleItemsList;

        private bool InventaryIsOpen = false;
        private int crutch = 0;

        private void Start()
        {
            Weaponlist.SetActive(true);
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (InventaryIsOpen == true && crutch == 0)
                {
                    Hotbar.SetActive(true);
                    Inventary.SetActive(false);
                    InventaryIsOpen = false;
                    crutch = 1;

                }
            }

            if (Input.GetKeyDown(KeyCode.I))
                {
                  if (InventaryIsOpen == false && crutch == 0)
                  {
                    Hotbar.SetActive(false);
                    Inventary.SetActive(true);
                    InventaryIsOpen = true;
                    crutch = 1;
                  }
            }
            crutch = 0;
        }

        public void Weapon() 
        {
           Weaponlist.SetActive(true);
           Skilllist.SetActive(false);
          UsebleItemsList.SetActive(false);

        }

        public void Skill()
        {
            Weaponlist.SetActive(false);
            Skilllist.SetActive(true);
            UsebleItemsList.SetActive(false);
        }

        public void UsebleItems() 
        {
            Weaponlist.SetActive(false);
            Skilllist.SetActive(false);
            UsebleItemsList.SetActive(true);
        }



    }
}
