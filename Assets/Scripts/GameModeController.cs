using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Platformer.Mechanics;
using UnityEditor;

public class GameModeController : MonoBehaviour
{

    public Platformer.Mechanics.GameMode activeMode;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();


    // Start is called before the first frame update
    void Start()
    {
        //player setup
        model.player.maxSpeed = activeMode.playerCharacterSettings.maxSpeed;
        model.player.jumpTakeOffSpeed = activeMode.playerCharacterSettings.jumpTakeOffSpeed;
        model.player.health.maxHP = activeMode.playerCharacterSettings.maxHP;
        model.player.health.setCurrentHP(activeMode.playerCharacterSettings.startingHP);
        model.player.invincibleTime = activeMode.playerCharacterSettings.invincibleTime;
        model.player.bulletDamage = activeMode.playerCharacterSettings.bulletDamage;

        //starting location
        model.player.transform.position = activeMode.start;

        //enemy setup
        foreach (EnemyController e in FindObjectsOfType(typeof(EnemyController)))
        {
            e.damage = activeMode.enemyDamage;
            if (e.GetComponent<Health>() != null)
            {
                e.GetComponent<Health>().maxHP = activeMode.enemyHealth;
                e.GetComponent<Health>().setCurrentHP(activeMode.enemyHealth);
            }

        }

    }

}
