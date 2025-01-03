using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPredfab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int bulletDamage = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPredfab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if(bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
        }
    }
    

    
}
