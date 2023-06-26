using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numberOfSpawns;
    [SerializeField] private int numberOfRounds;
    [SerializeField] private float timeBetweenRounds;
    [SerializeField] private float timeBetweenSpawns;
    
    private int currentRound = 0;
    private int spawnsRemaining;
    private bool spawnCompleted= false;

    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        currentRound++;
        spawnsRemaining = numberOfSpawns;

        SpawnPrefab();

    }

    private void SpawnPrefab()
    {

        if (spawnsRemaining <= 0)
        {
            if (currentRound >= numberOfRounds)
            {
                Debug.Log("Spawning completed");
                spawnCompleted = true;
            }
            else
            {
                StartCoroutine(StartNextRound());
            }
        }
        else
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            spawnsRemaining--;
            StartCoroutine(SpawnNextPrefab());
        }
    }

    private System.Collections.IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        StartRound();
    }

    private System.Collections.IEnumerator SpawnNextPrefab()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnPrefab();
    }
}

