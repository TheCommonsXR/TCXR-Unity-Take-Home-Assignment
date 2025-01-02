using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 100;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        public Text healthText;

        private void Awake()
        {
            currentHP = maxHP;
            UpdateHealthUI();
        }

        /// <summary>
        /// Starts entity with MaxHP
        /// </summary>
        private void Start()
        {
            currentHP = maxHP;
            UpdateHealthUI();
        }
        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>

        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthUI();
        }

        ///<summary>
        ///apply damage to entity. will trigger a HealthIsZero event when
        /// current HP reaches 0.
        ///</summary>
        public void TakeDamage(int damage)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            UpdateHealthUI();
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) TakeDamage(1);
        }

        ///<summary>
        ///update the Ui to show current HP
        ///</summary>
        void UpdateHealthUI()
        {
            if(healthText != null)
            {
                healthText.text = "Health: " + currentHP;
            }
        }
    }
}
