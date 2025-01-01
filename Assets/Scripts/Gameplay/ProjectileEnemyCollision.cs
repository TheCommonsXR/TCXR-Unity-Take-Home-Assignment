using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    public class ProjectileEnemyCollision : Simulation.Event<ProjectileEnemyCollision>
    {
        public EnemyController enemy;
        public Projectile projectile;

        public override void Execute()
        {
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(projectile.damage);
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
            }
        }
    }
}