using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameMode", menuName = "Game Mode Creator")]

public class GameMode : ScriptableObject
{
    [Header("Name of the gamemode you wish.")]
    public string modeName;

    [Header("Player health information")]
    public int playerHealth = 5;
    public bool canTakeDamage = true;
    public float invulnerabilityTime = 1f;

    [Header("Player movement floats")]
    public float playerMaxSpeed = 7f;
    public float playerJumpSpeed = 7f;

    [Header("Projectile settings")]
    public int projectileDamage = 5;
    public float projectileDespawnTime = 1f;

    [Header("Player Starting Location")]
    public Vector3 startingPosition = Vector3.zero;



}
