using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class spawner : MonoBehaviour
{
    public GameObject[] prefabs; // Use plural "prefabs"

    private enemy Enemy;
    private coins Coins;
    private Coroutine throwCoroutine;
    public float direction;

    public Vector3 spawnLocation = new Vector3(-5f, 1f, 0f);

    public float minTime = 1; // The minimum time to wait until throwing new items
    public float maxTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        Coins = prefabs[0].GetComponent<coins>();
        Enemy = prefabs[1].GetComponent<enemy>();
        // Coins.moveSpeed = speed;
        // Enemy.moveSpeed = speed;
        StartCoroutine(ThrowLoop());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ThrowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(maxTime);
            Create();
        }
    }

    public void Create()
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        // Store the original move speeds
        float originalEnemySpeed = Enemy.moveSpeed;
        float originalCoinsSpeed = Coins.moveSpeed;

        // Modify the move speeds based on direction
        Enemy.moveSpeed *= direction;
        Coins.moveSpeed *= direction;

        // Instantiate the chosen prefab
        GameObject go = Instantiate(prefabs[randomIndex], spawnLocation, Quaternion.identity);
        AudioManager.AudioController.PlayCommand(AudioManager.AudioController.pipeOut);
        // Reset the move speeds to their original values
        Enemy.moveSpeed = originalEnemySpeed;
        Coins.moveSpeed = originalCoinsSpeed;
    }
}
