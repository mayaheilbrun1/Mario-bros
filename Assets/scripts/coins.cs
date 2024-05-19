using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class coins : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity; 
    public float moveSpeed = 5f;
    private Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.platformFall);
        }
        if(other.gameObject.CompareTag("mario") || other.gameObject.CompareTag("luigi"))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            moveSpeed = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            gameManager.Instance.UpScore(other.gameObject.tag, "coin");
            animator.SetTrigger("collected");
            Invoke("Die", 0.3f);
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
