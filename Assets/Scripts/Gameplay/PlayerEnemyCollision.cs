using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        public BulletScript bullet;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(1);
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }


            else
            {
                if (player.immunityOn == false)
                {
                    player.StartImmunity();
                    var playerHealth = player.health;
                    
                    if (playerHealth != null)
                    {
                        playerHealth.Decrement(enemy.damage);

                        if (!playerHealth.IsAlive)
                        {
                            Schedule<PlayerDeath>();

                        }
                    }
                    else
                    {
                        Schedule<PlayerDeath>();
                    }
                    Debug.Log("Player Health: " + playerHealth.currentHP.ToString());

                }
            }
        }
    }
}