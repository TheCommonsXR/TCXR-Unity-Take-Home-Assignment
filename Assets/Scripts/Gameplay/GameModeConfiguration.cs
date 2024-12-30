using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Mode", menuName = "Game Mode Configuration")]
public class GameModeConfiguration : ScriptableObject
{
    public string gameModeName;

    // Player Values
    public int playerHealth;
    public int playerDamage;
    public float playerJumpTakeOffSpeed;
    public float playerMaxSpeed;
    public float projectileSpeed;
    public Vector2 playerStartingPosition;
    public float collisionImmunityTimer;

    // Enemy Values
    public int enemyDamage;
}
