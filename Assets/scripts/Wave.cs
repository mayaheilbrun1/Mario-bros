using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.AudioController.PlayCommand(AudioManager.AudioController.wave);
        Invoke("Die",0.3f);
    }

    // Update is called once per frame

    void Die()
    {
        Destroy(gameObject);
    }
}
