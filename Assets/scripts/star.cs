using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("Die",1.5f);
    }

    // Update is called once per frame

    void Die()
    {
        Destroy(gameObject);
    }
}
