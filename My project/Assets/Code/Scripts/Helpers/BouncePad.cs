using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed, ForceMode2D.Impulse);
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D ballRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        ballRigidBody.AddForce(ballRigidBody.velocity.normalized * speed, ForceMode2D.Impulse);
    }


}
