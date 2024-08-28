namespace Adventurer
{
    public class Enemy : NPC
    {
        float Damage { get; set; }
        public Enemy(float health, string name, float damage) : base (health, name)
        {
            damage = Damage;
        }

        public void Attack(NPC target)
        {
            target.TakeDamage(Damage);
        }
        public override void Move()
        {
            base.Move();
        }
    }
}
