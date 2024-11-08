using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Adventurer
{
    public class InventoryPartsView : MonoBehaviour
    {
        [SerializeField] private ItemCaseMirror part;
        [SerializeField] private Transform partsParent;
        [SerializeField] private List<CaseBrain> caseBrains = new List<CaseBrain>();
       
        private float partsAmount => caseBrains.Count;
        private int a = 0;
        private float HowManyStart;
        private List<ItemCaseMirror> caseMiracles = new List<ItemCaseMirror>();
        private int lastKey = 0;

        void Start()
        {
            if (partsAmount % 2 == 0)
            {
                a = 50;
                HowManyStart = partsAmount / 2;

                for (int i = 0; i < HowManyStart; i++)
                {
                    var spa = Instantiate(part, partsParent.position + new Vector3(a, 0, 0), Quaternion.identity);
                    spa.transform.SetParent(partsParent);
                    caseMiracles.Add(spa);
                    var sp = Instantiate(part, partsParent.position + new Vector3(-a, 0, 0), Quaternion.identity);
                    sp.transform.SetParent(partsParent);
                    caseMiracles.Add(sp);
                    a = a + 100;
                }
            }
            else
            {
                HowManyStart = partsAmount / 2 + 0.5f;

                for (int i = 0; i < HowManyStart; i++)
                {
                    var spawn = Instantiate(part, partsParent.position + new Vector3(a, 0, 0), Quaternion.identity);
                    spawn.transform.SetParent(partsParent);
                    caseMiracles.Add(spawn);
                    
                    if (a != 0)
                    {
                        var spaw = Instantiate(part, partsParent.position + new Vector3(-a, 0, 0), Quaternion.identity);
                        spaw.transform.SetParent(partsParent);
                        caseMiracles.Add(spaw);
                    }

                    a = a + 100;
                }
            }

            caseMiracles = caseMiracles.OrderBy(part => part.transform.position.x).ToList();

            for (int i = 0; i < caseBrains.Count; i++)
            {
                caseMiracles[i].Initialize(caseBrains[i]);
            }

            ChoosePart(0);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && caseMiracles.Count >= 1)
            {
                ChoosePart(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && caseMiracles.Count >= 2)
            {
                ChoosePart(1); 
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && caseMiracles.Count >= 3)
            {
                ChoosePart(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && caseMiracles.Count >= 4)
            {
                ChoosePart(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && caseMiracles.Count >= 5)
            {
                ChoosePart(4); 
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && caseMiracles.Count >= 6)
            {
                ChoosePart(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && caseMiracles.Count >= 7)
            {
                ChoosePart(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && caseMiracles.Count >= 8)
            {
                ChoosePart(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && caseMiracles.Count >= 9)
            {
                ChoosePart(8);
            }
        }

        private void  ChoosePart(int index)
        {
            caseMiracles[lastKey].transform.GetChild(0).gameObject.SetActive(false);
            caseMiracles[index].transform.GetChild(0).gameObject.SetActive(true);
            lastKey = index;
        }
    }
}
