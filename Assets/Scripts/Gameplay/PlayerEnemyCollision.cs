using Platformer.Core; // Provides the Simulation framework for events.
using Platformer.Mechanics; // Includes core mechanics like Health and PlayerController.
using Platformer.Model; // Contains the game model for shared configurations.
using UnityEngine; // Unity's core library for game development.
using static Platformer.Core.Simulation; // Allows simplified use of Simulation.Schedule<T>().

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// Handles damage and death logic for both the player and enemy.
    /// </summary>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy; // Reference to the enemy involved in the collision.
        public PlayerController player; // Reference to the player involved in the collision.

        PlatformerModel model = Simulation.GetModel<PlatformerModel>(); // Access the shared game model.

        /// <summary>
        /// Executes the collision logic when triggered.
        /// </summary>
        public override void Execute()
        {
            // Determine if the player is attacking the enemy from above.
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy) // If the player is attacking from above.
            {
                // Attempt to get the enemy's health component.
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null) // If the enemy has a Health component.
                {
                    enemyHealth.TakeDamage(1); // Deal 1 damage to the enemy.
                    if (!enemyHealth.IsAlive) // If the enemy's health is 0.
                    {
                        Schedule<EnemyDeath>().enemy = enemy; // Schedule the EnemyDeath event.
                        player.Bounce(2); // Make the player bounce slightly.
                    }
                    else
                    {
                        player.Bounce(7); // Make the player bounce higher if the enemy survives.
                    }
                }
                else // If the enemy does not have a Health component.
                {
                    Schedule<EnemyDeath>().enemy = enemy; // Schedule the EnemyDeath event.
                    player.Bounce(2); // Make the player bounce slightly.
                }
            }
            else // If the player is not attacking from above.
            {
                // Attempt to get the player's health component.
                var playerHealth = player.GetComponent<Health>();
                if (playerHealth != null && playerHealth.IsAlive) // If the player has health and is alive.
                {
                    playerHealth.TakeDamage(1); // Deal 1 damage to the player.
                    if (!playerHealth.IsAlive) // If the player's health reaches 0.
                    {
                        Schedule<PlayerDeath>(); // Schedule the PlayerDeath event.
                    }
                }
            }
        }
    }
}