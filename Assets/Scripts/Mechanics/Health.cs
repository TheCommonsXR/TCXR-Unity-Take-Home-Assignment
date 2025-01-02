using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.UI;
using System.Collections;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 3; //MAX/STARTING HEALTH     Q1
        public int currentHP;
        public Image healthBarFill;

        public bool isImmune = false; // TRACKS IMMUNITY STATE      Q2
        public float immunityDuration = 1f; // DURATION OF IMMUNITY     Q2



        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        void Awake() //RUNS BEFORE THE GAME STARTS
        {
            currentHP = maxHP;//        Q1
            UpdateHealthBar();//        Q1
        }
        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        /// 
        private void Start()
        {

        }
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthBar();
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0) // IF HEALTH IS ZERO, PLAYER DIES       Q1
            {
                if (this.CompareTag("Player"))
                {
                    Schedule<PlayerDeath>();
                }
            }
            UpdateHealthBar();
        }

        public IEnumerator GrantImmunity() // CREATES FLASHING EFFECT TO INDICATE IMMUNITY AFTER COLLISION      Q2
        {
            isImmune = true;
            var renderer = GetComponent<SpriteRenderer>();
            for (float i = 0; i < immunityDuration; i += 0.2f)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            renderer.enabled = true; // MAKES SURE PLAYER IS VISIBLE AFTER IMMUNITY
            isImmune = false;
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        private void UpdateHealthBar() // UPDATES HEALTH BAR UI AFTER ANY INCREASE OR DECREASE OF HEALTH        Q1
        {
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = (float)currentHP / maxHP;
            }

        }
    }
}
