using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    public int damageD;

    public float bulletDespawn;

    private void Start()
    {
        Destroy(gameObject, bulletDespawn);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           var holder = collision.gameObject.GetComponent<Health>();
            holder.Decrement(damageD);
        }
    }
}
