using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounded : MonoBehaviour
{
    public bool isGrounded => groundInt > 0;

    private int groundInt;
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // print(isGrounded);

        AudioManager.AudioController.PlayCommand(AudioManager.AudioController.platformFall);
        
        groundInt++;
        // isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        groundInt--;
        // isGrounded = false;
    }
    
    
}
