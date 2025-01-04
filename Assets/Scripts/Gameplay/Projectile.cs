using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    public int damageD;// damage dealt
    public float bulletDespawn; // bullet despawn time

    private void Start()
    {
        if (GameModeManager.Instance != null && GameModeManager.Instance.currentGameMode != null) {
            damageD = GameModeManager.Instance.currentGameMode.projectileDamage;
            bulletDespawn = GameModeManager.Instance.currentGameMode.projectileDespawnTime;
        }
        Destroy(gameObject, bulletDespawn);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var holder = collision.gameObject.GetComponent<Health>();
            if (holder != null)
            {
                holder.Decrement(damageD);
                Destroy(gameObject);
            }
        }
    }

    public void ConfigureProjectile(int damageValue, float despawnValue)
    {
        damageD = damageValue;
        bulletDespawn = despawnValue;
    }
}
