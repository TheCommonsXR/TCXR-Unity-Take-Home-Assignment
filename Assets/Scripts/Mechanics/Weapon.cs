using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Question #3
    /// Added Weapon Class for the weapon to shoot, have a cool down, and bind it to a key on keyboard
    /// </summary>

    public Transform firePoint;
    public GameObject bulletPrefab;

    const float coolDownTimeOut = 1f;
    float coolDown = coolDownTimeOut;

    // Update is called once per frame
    void Update()
    {
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            Debug.Log(coolDown, this);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(coolDown < 0)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        coolDown = coolDownTimeOut;
        Bullet projectile = GameObject.Instantiate(bulletPrefab, firePoint.position,firePoint.rotation).GetComponent<Bullet>();

        Vector2 shootDir = transform.right;

        projectile.rb.velocity = shootDir * projectile.speed;
    }
}
