using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using static Platformer.Core.Simulation;
using Platformer.Gameplay;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameModeConfiguration currentGameMode;
    public List<EnemyController> enemyList = new List<EnemyController>();

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGameMode();
    }

    private void StartGameMode()
    {
        if (currentGameMode != null)
        {
            model.player.transform.position = currentGameMode.playerStartingPosition;
            model.player.GetComponent<Health>().SetMaxHealth(currentGameMode.playerHealth);

            var playerController = model.player.GetComponent<PlayerController>();
            playerController.jumpTakeOffSpeed = currentGameMode.playerJumpTakeOffSpeed;
            playerController.maxSpeed = currentGameMode.playerMaxSpeed;
            playerController.SetPlayerDamage(currentGameMode.playerDamage);
            playerController.SetProjectileVelocity(currentGameMode.projectileSpeed);
            playerController.SetCollisionImmunityTimer(currentGameMode.collisionImmunityTimer);
        }

        foreach (EnemyController enemy in enemyList)
        {
            enemy.SetEnemyDamage(currentGameMode.enemyDamage);
        }
    }
}
