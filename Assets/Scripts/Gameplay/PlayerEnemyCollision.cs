using Platformer.Core;
using System;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
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
        public int jumpDamage = 10;// set base of 10 to make sure it dies
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(jumpDamage);
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
                Debug.Log("is this instantly killing me?");
                player.playerHealth.Decrement();
                player.playerHealth.TriggerInvul();
            }
        }

    }

}