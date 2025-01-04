using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameMode currentGameMode;
    public PlayerController player;

    void Start()
    {
        if(currentGameMode != null)
        {
            ActivateGameMode(currentGameMode);
        }
        else 
        {
            Debug.LogWarning("No game mode is assigned so thats not right!");
        }
    }
    public void ActivateGameMode(GameMode mode)
    {
        currentGameMode = mode;
        if (currentGameMode == null || player == null) return;
        var playerHealth = player.playerHealth;
        if (playerHealth != null)
        {
            playerHealth.maxHP = currentGameMode.playerHealth;
            playerHealth.canTakeDamage = currentGameMode.canTakeDamage;
            playerHealth.InvulnDuration = currentGameMode.invulnerabilityTime;
            // this is only if we need to make sure the player hp is correct it will kill the player though and then reset it so i wanted to keep it but commented out.
            //playerHealth.Die();// makes is alive false
            //playerHealth.Awake();// resets health

        }

        //player movement config
        player.maxSpeed = currentGameMode.playerMaxSpeed;
        player.jumpTakeOffSpeed = currentGameMode.playerJumpSpeed;

        //starting position config
        player.transform.position = currentGameMode.startingPosition;


        var projectilePrefabsScript = player.projectilePrefab.GetComponent<Projectile>();
        if (projectilePrefabsScript != null)
        {
            projectilePrefabsScript.ConfigureProjectile(

                currentGameMode.projectileDamage,
                currentGameMode.projectileDespawnTime
                );
        }
        Debug.Log("current game mode is: " + currentGameMode.modeName);
    }
}
