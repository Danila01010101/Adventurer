namespace Adventurer
{
    public class Enemy : NPC
    {
        public float Damage;

        public void Attack(NPC target)
        {
            target.TakeDamage(Damage);
        }
        public override void Move()
        {
            
        }
    }
}
