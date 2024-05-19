using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headCollider : MonoBehaviour
{
    public GameObject wave;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("floor"))
        {
            
            Vector3 collisionPoint = other.contacts[0].point;
            collisionPoint.y += 0.5f;
            GameObject go = Instantiate(wave, collisionPoint, Quaternion.identity);
            // go.gameObject.tag = other.gameObject.tag+"wave";

        }
    }
}
