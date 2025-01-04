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
    public class PlayerHealth: MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        [Header("This is the area where you set max hp")] 
        public int maxHP ; // increased to a basis of 5 this is still changeable in editor but i wanted to make a minimum of a higher value here to
        // this is also an increase to the enemy so i lowered it back down to 1 for them in the editor

        [Header("Shows when taking damage is possible collision is still on but you wont die")]
        public bool canTakeDamage = true;

        [Header("How long  you will be immortal for in seconds")]
        public float InvulnDuration ;

        [Header("Any enemy controller for their damage number")]

        public EnemyController enemy;




        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool isAlive = true;
        int currentHP;



        public void Awake()
        {
            if (GameModeManager.Instance != null && GameModeManager.Instance.currentGameMode != null)
            {
                maxHP = GameModeManager.Instance.currentGameMode.playerHealth;
                InvulnDuration = GameModeManager.Instance.currentGameMode.invulnerabilityTime;
                canTakeDamage = GameModeManager.Instance.currentGameMode.canTakeDamage;
            }
            currentHP = maxHP;
        }
            /// <summary>
            /// Increment the HP of the entity.
            /// </summary>
            public void Increment()
        {
            while(currentHP < maxHP)
            {

                currentHP++;
                Debug.Log(currentHP);
            }
            return;
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            Debug.Log("Current HP" + currentHP);
            if (!canTakeDamage)
            {
                Debug.Log("Are we returning in Decrement?");
                return;
            }
            else
            {
                var enemyDamage = enemy.GetComponent<Health>();
                currentHP -= enemyDamage.damage;
                Debug.Log("Health after enemy damage is applied to current hp");
                if (currentHP <= 0) // just incase something does more damage, and <= refused to work.
                {
                    isAlive = false;
                    var ev = Schedule<HealthIsZero>();
                    ev.playerHealth = this;
                }
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>

        public void Die()
        {
            currentHP = 0;
            if (currentHP == 0)
            {
                isAlive = false;
                var ev = Schedule<HealthIsZero>();
                ev.playerHealth = this;
            }
        }


        public void TriggerInvul()
        {
            if (canTakeDamage)
            {
                StartCoroutine(BecomeImmortal());
            }
        }
        private IEnumerator BecomeImmortal()
        {
            canTakeDamage = false;
            yield return new WaitForSeconds(InvulnDuration);
            canTakeDamage = true;

        }
    }


}
