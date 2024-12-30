using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Bullet : MonoBehaviour
{

    public float velocity;

    public float deleteTime = 5;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    

    void Awake()
    {
        GetComponent<SpriteRenderer>().flipX = model.player.GetComponent<SpriteRenderer>().flipX;
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            velocity *= -1;
        }

        StartCoroutine("deleteBullet");

    }

    IEnumerator deleteBullet()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + velocity, transform.position.y);
    }
}
