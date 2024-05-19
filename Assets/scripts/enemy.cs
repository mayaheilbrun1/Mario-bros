using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Vector2 velocity; 
    public float moveSpeed = 5f;
    public bool isFacingLeft = true;

    private Animator _animator;

    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(moveSpeed, rb.velocity.y);
        Flip();
    }
    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }
    private void Flip()
    {
        if ((isFacingLeft && moveSpeed < 0f) || (!isFacingLeft && moveSpeed > 0f))
        {
            isFacingLeft = !isFacingLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.platformFall);
        }
        if (other.gameObject.CompareTag("wave"))
        {
            if(!hit)
            {
                _animator.SetTrigger("hit");
                hit = true;
                moveSpeed = 0;
                gameObject.tag = "deadenemy";
            }
        }
        else if ((other.gameObject.CompareTag("mario") || other.gameObject.CompareTag("luigi")) && hit)
        {
            GetComponent<Collider2D>().enabled = false;
            moveSpeed = 1;
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.enemyDie);
            gameManager.Instance.UpScore(other.gameObject.tag, "enemy");
            Invoke("Die", 5f);
        }
        
        else if (other.gameObject.CompareTag("mario"))
        {
            print( "marioInvicible"+gameManager.Instance.marioInvicible);
            if (gameManager.Instance.marioInvicible)
            {
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.enemyDie);
                print( "marioInvicible"+gameManager.Instance.marioInvicible);
                GetComponent<Collider2D>().enabled = false;
                moveSpeed = 1;
                gameManager.Instance.UpScore(other.gameObject.tag, "enemy");
                Invoke("Die", 5f);
            }
        }
        else if (other.gameObject.CompareTag("luigi"))
        {
            if (gameManager.Instance.luigiInvicible)
            {
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.enemyDie);
                print( "luigiInvicible"+gameManager.Instance.luigiInvicible);
                GetComponent<Collider2D>().enabled = false;
                moveSpeed = 1;
                gameManager.Instance.UpScore(other.gameObject.tag, "enemy");
                Invoke("Die", 5f);
            }
            
        }
        
        
        
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
}
