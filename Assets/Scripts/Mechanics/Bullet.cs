using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

/// <summary>
/// Question #3
/// Created Bullet class to come out the weapon, to have collision on enemies, to destroy when it comes in contact with enemy or goes off screen of the camera view.
/// To do damage to the enemy!
/// </summary>

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10f;
    private Vector2 screenBounds;
    public Rigidbody2D rb;
    private Camera cam;
    public EnemyController enemy;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        DestroyWhenOffScreen();    
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);

        if(screenPos.x < 0 || screenPos.x > cam.pixelWidth || screenPos.y < 0 || screenPos.y > cam.pixelHeight)
        {
            Destroy(this.gameObject);
        }
    }

}
