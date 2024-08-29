namespace Adventurer
{
    public class Enemy : NPC
    {
        public float Damage { get; set; }

        public Enemy(float health, string name, float damage) : base (health, name)
        {
            Damage = damage;
        }

        public void Attack(NPC target)
        {
            target.TakeDamage(Damage);
        }
        public override void Move()
        {
            
        }
    }
}
