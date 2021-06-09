using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSoulMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private Vector2 startMovement;
    public Vector2 maxSpeed;
    public float knockbackForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(startMovement.x * maxSpeed.x, startMovement.y * maxSpeed.y), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Mathf.Abs(rb.velocity.y) < maxSpeed.y)
        {
            rb.AddForce(new Vector2(0, rb.velocity.y * maxSpeed.y), ForceMode2D.Impulse);
        }

        if (Mathf.Abs(rb.velocity.x) < maxSpeed.x)
        {
            rb.AddForce(new Vector2(rb.velocity.x * maxSpeed.x, 0), ForceMode2D.Impulse);   
        }
        
    }
}
