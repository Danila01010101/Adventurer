using UnityEngine;

namespace Adventurer
{
    public class Enemy : NPC
    {
        public float Damage;

        public override void Start()
        {
            base.Start();
            Attack(this);
        }
        public void Attack(NPC target)
        {
            target.TakeDamage(Damage);
            Debug.Log(Health);
        }
    }
}
