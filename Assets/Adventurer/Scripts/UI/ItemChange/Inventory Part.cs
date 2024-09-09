using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Adventurer
{
    public class InventoryPart : MonoBehaviour
    {
        [SerializeField] private GameObject part;
        [SerializeField] private float HowManyPart = 1;
        [SerializeField] private GameObject CanvasObject;
       
        private int a = 0;
        private float HowManyStart;
        private List<GameObject> parts = new List<GameObject>();
        private int lastKey = 0;

        void Start()
        {
            if (HowManyPart % 2 == 0)
            {
                a = 1;

                HowManyStart = HowManyPart / 2;
                for (int i = 0; i < HowManyStart; i++)
                {

                    //var spa = Instantiate(part, CanvasObject.transform.position + new Vector3(a, 0, 0), Quaternion.identity);
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
            ChoosePart(0);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && parts.Count >= 1)
            {
                ChoosePart(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && parts.Count >= 2)
            {
                ChoosePart(1); 
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && parts.Count >= 3)
            {
                ChoosePart(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && parts.Count >= 4)
            {
                ChoosePart(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && parts.Count >= 5)
            {
                ChoosePart(4); 
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && parts.Count >= 6)
            {
                ChoosePart(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && parts.Count >= 7)
            {
                ChoosePart(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && parts.Count >= 8)
            {
                ChoosePart(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && parts.Count >= 9)
            {
                ChoosePart(8);
            }
        }

        private void  ChoosePart(int index)
        {
            parts[lastKey].transform.GetChild(0).gameObject.SetActive(false);
            parts[index].transform.GetChild(0).gameObject.SetActive(true);
            lastKey = index;
        }
    }
}
