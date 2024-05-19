using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class startSpawner : MonoBehaviour
{

    public GameObject starPrefab;

    private float[] Y_Axis = { 2f, -0.2f, 0.01f };

    private float x_Axis_max = 7.4f;

    private float x_Axis_min = -7.4f;
    
    private Coroutine starCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        starCoroutine = StartCoroutine(ThrowLoop());
    }
    
    
    IEnumerator ThrowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);
            startaStar();
        }
    }


    void startaStar()
    {
        float x_position = Random.Range(x_Axis_min, x_Axis_max);
        float y_position = Y_Axis[Random.Range(0, 3)];
        GameObject star = Instantiate(starPrefab, new Vector3(x_position, y_position, 0), Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
