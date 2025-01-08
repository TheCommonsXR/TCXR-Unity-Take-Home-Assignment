using Platformer.Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameMode[] gameModes;
    private GameMode currentGameMode;

    void Start()
    {
        SetGameMode(gameModes[0]);
    }

    public void SetGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        ApplyGameMode(GetCurrentGameMode());
    }

    private void ApplyGameMode(GameMode gameMode)
    {
        throw new NotImplementedException();
    }

    private GameMode GetCurrentGameMode()
    {
        return currentGameMode;
    }

    [Obsolete]
    private void ApplyGameMode()
    {
        // Apply the game mode's configuration to the game
        // For example, setting player health and starting position
        player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            Health playerHealthComponent = player.GetComponent<Health>();
            if (playerHealthComponent != null)
            {
                playerHealthComponent.SetHealth(currentGameMode.currentHp); // Use the SetHealth method
            }
            else
            {
                Debug.LogWarning("Player Health component not found!");
            }
            player.transform.position = currentGameMode.startingPosition;

            PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            if (playerShooting != null) { 
                playerShooting.bulletDamage = currentGameMode.bulletDamage; 
                playerShooting.bulletPrefab = currentGameMode.bulletPrefab; 
            } else { 
                Debug.LogWarning("PlayerShooting component not found!"); 
            }
        } else { 
            Debug.LogWarning("Player object not found in the scene!"); }
    }
}


