using System.Collections.Generic;
using UnityEngine;

namespace Adventurer
{
    public class  Product
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public Product(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
    public class Merchant : NPC
    {
        public GameObject Window;
        public List<Product> Products { get; set; }
        public Merchant(string name,float health, List<Product> products, GameObject window) :base (health,name)
        {
            Products = products;
            Window = window;
        }

        public void OpenUI() 
        {
            Window.SetActive(!Window.activeInHierarchy);
        }

        public void Buy(string product, int count, int countbuy)
        {
            UpdateCountProduct(product, count - countbuy);
        }
        public void UpdateCountProduct(string name, int newCount)
        {
            foreach (var item in Products)
            {
                if (item.Name==name) 
                {
                    item.Count=newCount;
                }
            }
        }
    }
}