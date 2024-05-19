using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Destructor : MonoBehaviour
{
    // This method is called when this object collides with another object
    public float direction;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a tag of "coins" or "turtle"
        if (collision.gameObject.CompareTag("coins") || collision.gameObject.CompareTag("turtle"))
        {
            // Print the tag of the collided object
            
            
            // Destroy the collided object
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("mario") || collision.gameObject.CompareTag("luigi"))
        {
            collision.gameObject.transform.position = new Vector3(-(collision.gameObject.transform.position.x)+direction,
                collision.gameObject.transform.position.y+0.1f,0f);


        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("coins") || collision.gameObject.CompareTag("turtle"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("mario") || collision.gameObject.CompareTag("luigi"))
        {
            collision.gameObject.transform.position = new Vector3(-(collision.gameObject.transform.position.x)+direction,
                collision.gameObject.transform.position.y+0.1f,0f);

        }
    }
}