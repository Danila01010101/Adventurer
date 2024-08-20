using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Adventurer
{
    public class InventoryPart : MonoBehaviour
    {

        public GameObject part;
        public float HowManyPart = 1;
        public Canvas CanvasObject;
       
        private int a = 0;
        private InventaryPart _imput;
        private float HowManyStart;
        private List<GameObject> parts = new List<GameObject>();

        private int d;
        private int dk;

        void Start()
        {
            if (HowManyPart % 2 == 0)
            {
                a = 1;

                HowManyStart = HowManyPart / 2;
                for (int i = 0; i < HowManyStart; i++)
                {
                  
                    var spa = Instantiate(part, new Vector2(a, 0), Quaternion.identity);
                    spa.transform.SetParent(CanvasObject.transform);
                    parts.Add(spa);

                    var sp = Instantiate(part, new Vector2(-a, 0), Quaternion.identity);
                    sp.transform.SetParent(CanvasObject.transform);
                    parts.Add(sp); 
                    a = a + 2;

                  
    }
            }
            else
            {
                HowManyStart = HowManyPart / 2 + 0.5f;
                for (int i = 0; i < HowManyStart; i++)
                {

                    var spawn = Instantiate(part, new Vector2(a, 0), Quaternion.identity);
                    spawn.transform.SetParent(CanvasObject.transform);
                    parts.Add(spawn);
                    

                    if (a != 0)
                    {
                        var spaw = Instantiate(part, new Vector2(-a, 0), Quaternion.identity);
                        spaw.transform.SetParent(CanvasObject.transform);
                        parts.Add(spaw);
                       
                    }
                    a = a + 2;
                   
                   
                }

            }
            parts = parts.OrderBy(part => part.transform.position.x).ToList();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && parts.Count >= 1)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[0].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && parts.Count >= 2)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[1].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && parts.Count >= 3)
            {
                for (int i = 0; i < parts.Count ; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[2].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && parts.Count >= 4)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[3].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && parts.Count >= 5)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[4].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && parts.Count >= 6)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[5].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && parts.Count >= 7)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[6].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && parts.Count >= 8)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[7].transform.GetChild(0   ).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && parts.Count >= 9)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    parts[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                parts[8].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
