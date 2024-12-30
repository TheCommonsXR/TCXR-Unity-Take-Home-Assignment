using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float projectileVelocity;
    [HideInInspector] public bool flipX;
    [HideInInspector] public int damage;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (flipX)
        {
            transform.position += Vector3.left * projectileVelocity * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * projectileVelocity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            var ev = Schedule<ProjectileEnemyCollision>();
            ev.projectile = this;
            ev.enemy = enemy;
        }
    }
}
