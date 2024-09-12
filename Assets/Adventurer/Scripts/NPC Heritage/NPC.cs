using UnityEngine;

namespace Adventurer
{
    [RequireComponent(typeof(NPCMovement))]
    public class NPC : MonoBehaviour, IDamagable
    {
        public string Name;
        public float Health;

        protected NPCMovement movement;

        public virtual void Start()
        {
           movement = GetComponent<NPCMovement>();
           Debug.Log(movement.gameObject);
        }
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
        public virtual void Update()
        {
            Move();
            if (Health<=0)
            {
                NPCDeath();
            }
        }
        public virtual void Move()
        {

        }
        public virtual void NPCDeath() 
        {

        }
    }
}