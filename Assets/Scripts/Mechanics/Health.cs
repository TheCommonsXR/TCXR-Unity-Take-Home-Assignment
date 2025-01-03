using System;
using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
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
        public int maxHP = 1; // increased to a basis of 5 this is still changeable in editor but i wanted to make a minimum of a higher value here to
        // this is also an increase to the enemy so i lowered it back down to 1 for them in the editor

        public int damage ;
        private Collider2D playerCollider;
        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);

        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>


        void Awake()
        {
            currentHP = maxHP;        }
    }
}
