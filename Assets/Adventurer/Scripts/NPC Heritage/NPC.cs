using UnityEngine;

namespace Adventurer
{
    public class NPC : MonoBehaviour, IDamagable
    {
        public string Name { get; set; }
        public float Health { get; set; }

        public NPC(float health, string name) 
        {
            Health = health;
            Name = name;
        }
        public void TakeDamage(float damage) 
        {
            Health -= damage;
        }
        public virtual void Move()
        {

        }
    }
}
