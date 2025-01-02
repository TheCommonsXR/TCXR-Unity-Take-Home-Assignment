using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using Platformer.Gameplay;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifespan = 2f;

    private Vector2 direction;

    public EnemyController enemy;

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized; // NORMALIZED SCALES VECTORS TO HAVE A MAGNITUDE OF 1        Q3
        Destroy(gameObject, lifespan);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // CONTINUES MOTION OF PROJECTILE       Q3
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // CHECKS IF PROJECTILE COLLIDED WITH ENEMY
        {
            var enemyHealth = collision.GetComponent<Health>(); // GETS ENEMY HEALTH       Q3
            var enemyController = collision.GetComponent<EnemyController>(); // GETS ENEMY CONTROLLER       Q3

            Schedule<EnemyDeath>().enemy = enemyController; // ENEMY DIES UPON IMPACT       Q3

            Destroy(gameObject); // PROJECTILE IS DESTROYED UPON IMPACT     Q3
        }
    }
    
}
