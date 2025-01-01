using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;

    private float forceAmt;
    public float healthDamage;
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (isRight)
            {
                rb.AddForce(Vector2.right * forceAmt, ForceMode2D.Impulse);
            }
            else if (!isRight)
            {
                rb.AddForce(Vector2.left * forceAmt, ForceMode2D.Impulse);

            }
        }
    }
    public void Instantiated(bool directionInput, float forceFactor, float damage)
    {
        isRight = directionInput;
        forceAmt = forceFactor;
        healthDamage = damage;
    }
}


