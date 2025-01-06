using System.Collections;
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


        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
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
                    Debug.Log("You did -1 hp to the enemy!");
                }
            }

            ///<summary>
            /// Question #1
            /// This gets the player health from the player then will see if it exists, and if it does it will go through decrease the amount of health of the player
            /// and if the player's health reaches zero the player will die then respawn back in the beginning.
            /// If it doesn't exist aka is null then player will die then respawn.
            /// </summary>
            else
            {
                var playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Decrement();
                    ///<summary>
                    ///Question #2
                    ///I put the immunity here because when the player should get hit by an enemy, the player shouldn't lose more damage after losing one hp.
                    /// </summary>
                    player.ImmunityActivated();
                    if (!playerHealth.IsAlive)
                    {
                        Schedule<PlayerDeath>();
                        Debug.Log("You died!");
                        Schedule<PlayerSpawn>();
                    }

                }
            }
        }
    }
}