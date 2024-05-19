using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float moveSpeed = 10f;
    public grounded isGrounded;
    public float jumpForce = 15f;
    private Rigidbody2D rb;
    private Animator animator;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    private Vector2 velocity; 
    public bool isFacingRight;
    private bool grounded;
    public Vector3 initialPosition;
    public string character;
    private Behaviour halo;
    
    
    // public AnimatorController Controller;
    private RuntimeAnimatorController Controller;
    public AnimatorOverrideController superController;
    private bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        // lastYposition = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false; // false
        Controller = animator.runtimeAnimatorController;
        

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = isGrounded.isGrounded;
        animator.SetBool("grounded",grounded);
        // Debug.Log(grounded);
        
        if (Input.GetKey(left))
        {
            velocity = new Vector2(-moveSpeed, rb.velocity.y);
            horizontal = -moveSpeed;
            animator.SetFloat("speed",moveSpeed);
            //GetComponent<Renderer>().flipX = true;
            
            
            
            
        }else if (Input.GetKey(right))
        {
            velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetFloat("speed",moveSpeed);
            horizontal = moveSpeed;
            //GetComponent<Renderer>().flipX = false;
        }
        else
        {
            // Debug.Log("up");
            velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("speed",0);
            

        }

        if (Input.GetKeyDown(jump)&&grounded)
        {
            Debug.Log("up"+grounded);
            jumping = true;
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.jump);
            
        }
        Flip();

        
        animator.SetFloat("velocityY", rb.velocity.y);
    }



    private void FixedUpdate()
    {
        if (jumping)
        {
            // Debug.Log("jumoung fixed");
            velocity = new Vector2(rb.velocity.x, jumpForce);
            jumping = false;
        }
        rb.velocity = velocity;
    }
    
    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            // set animation to go right/left
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("deadenemy"))
        {
            animator.SetTrigger("kick");
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.collect);
        }
        
        else if (other.gameObject.CompareTag("turtle"))
        {
            if (gameObject.CompareTag("mario"))
            {
                if (!gameManager.Instance.marioInvicible)
                {
                    Debug.Log(gameObject.tag + "died");
                    if (gameManager.Instance.marioLives > 1)
                    {
                        gameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
                    }
                    gameManager.Instance.lose_life(gameObject.tag);
                    
                    
                }
            }
            else if(gameObject.CompareTag("luigi"))
            {
                if (!gameManager.Instance.luigiInvicible)
                {
                    Debug.Log(gameObject.tag + "died");
                    if (gameManager.Instance.luigiLives > 1)
                    {
                        gameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
                    }
                    gameManager.Instance.lose_life(gameObject.tag);
                    
                    
                }
                
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.gameObject.CompareTag("star"))
        {
            halo.enabled = true; 
            Destroy(other.gameObject);
            print("star");
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.collectCoin);
            if (gameObject.CompareTag("mario"))
            {
                
                gameManager.Instance.changeMarioInvi();
            }
            else if (gameObject.CompareTag("luigi"))
            {
                gameManager.Instance.changeLuigiInvi();
            }

            animator.runtimeAnimatorController = superController;
            Invoke("changeControllerBack", 4f);
        }
    }

    void changeControllerBack()
    {
        
        animator.runtimeAnimatorController = Controller;
        halo.enabled = false; 
        // Time.de
    }

}
    

