using System.Collections; // Added for IEnumerator and coroutines.
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Health : MonoBehaviour
    {
        public int maxHP = 1; // Maximum health points for this entity.

        // Property to check if the entity is still alive (current health > 0).
        public bool IsAlive => currentHP > 0;

        private int currentHP; // Current health points.
        private bool isInvincible = false; // Tracks whether the entity is invincible.
        public float invincibilityDuration = 1f; // Duration of invincibility after taking damage.

        // Increments the current health, ensuring it doesn't exceed the maximum.
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Handles taking damage, reducing health points, and triggering invincibility.
        /// </summary>
        /// <param name="damage">Amount of damage to apply.</param>
        public void TakeDamage(int damage)
        {
            // If the entity is invincible, ignore the damage.
            if (isInvincible) return;

            // Reduce health by the specified damage amount, ensuring it doesn't drop below 0.
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

            // If health reaches zero, trigger the `HealthIsZero` event.
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>(); // Schedules the event using the Simulation framework.
                ev.health = this;
            }

            // Start invincibility coroutine to prevent further immediate damage.
            StartCoroutine(HandleInvincibility());
        }

        /// <summary>
        /// Coroutine to manage the invincibility period after taking damage.
        /// </summary>
        private IEnumerator HandleInvincibility()
        {
            isInvincible = true; // Set invincibility flag.

            // Optional: Provide visual feedback for invincibility by blinking the sprite.
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float elapsed = 0f;
                while (elapsed < invincibilityDuration)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle sprite visibility.
                    yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before toggling again.
                    elapsed += 0.1f; // Increment elapsed time.
                }
                spriteRenderer.enabled = true; // Ensure the sprite is visible after invincibility ends.
            }
            else
            {
                // If no SpriteRenderer is present, simply wait for the duration.
                yield return new WaitForSeconds(invincibilityDuration);
            }

            isInvincible = false; // End invincibility.
        }

        /// <summary>
        /// Reduces health to zero by repeatedly applying damage.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) TakeDamage(1); // Reduces health until it reaches zero.
        }

        /// <summary>
        /// Initializes health points when the object awakens.
        /// </summary>
        void Awake()
        {
            currentHP = maxHP; // Set current health to maximum at the start.
        }
    }
}