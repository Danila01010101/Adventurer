using UnityEngine;

namespace Adventurer
{
    [RequireComponent(typeof(CharacterMovement))]
    public class NPC : MonoBehaviour, IDamagable
    {
        public string Name;
        public float Health;

        protected CharacterMovement movement;

        private void Start()
        {
            CharacterMovement movement = GetComponent<CharacterMovement>();
        }
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
        private void Update()
        {
            Move();
            if (Health<=0)
            {
                NPCDeath();
            }
        }
        public virtual void Move()
        {
            movement.HandleMovement();
            movement.HandleGroundCheck();
            movement.HandleGravity();
            movement.HandleAnimation();
        }
        public virtual void NPCDeath() 
        {

        }
    }
}