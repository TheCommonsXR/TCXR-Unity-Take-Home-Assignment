using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// Represebts the current vital statistics of some game entity.
    public class Health : MonoBehaviour
    {
        /// The maximum hit points for the entity.
        [SerializeField] private int maxHP = 1;
        [SerializeField] private int currentHP;

        /// Indicates if the entity should be considered 'alive'.
        public bool IsAlive => currentHP > 0;

        public void Respawn()
        {
            currentHP = maxHP;
        }

        /// Increment the HP of the entity.
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        public void Decrement(int damage)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }

            Debug.Log($"{this.gameObject.name} took {damage} damage and now has {currentHP} current HP.");
        }

        /// Decrement the HP of the entitiy until HP reaches 0.
        public void Die()
        {
            while (currentHP > 0) Decrement(1);
        }

        void Awake()
        {
            currentHP = maxHP;
        }

        public void SetMaxHealth(int health)
        {
            maxHP = health;
        }

        public int GetCurrentHealth => currentHP;
    }
}
