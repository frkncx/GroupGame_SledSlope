using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] chunks;
    private Transform lastEndPoint;

    private bool canSpawn = true;

    [Header("Spawning Settings")]
    public float checkThreshold = 0.1f;
    private  float spawnCooldown = .001f;

    private void Start()
    {
        SpawnInitialChunk();
    }

    private void Update()
    {
        if (lastEndPoint == null) return;

        if (lastEndPoint.position.x <= 15f + checkThreshold && canSpawn)
        {
            SpawnNextChunk();
            StartCoroutine(SpawnCooldown());
        }
    }

    private void SpawnInitialChunk()
    {
        GameObject initialChunk = Instantiate(chunks[0], new Vector2(-5.2f, -1.93f), Quaternion.identity, transform);
        lastEndPoint = initialChunk.transform.Find("EndPoint");
    }

    private void SpawnNextChunk()
    {
        GameObject nextChunkPrefab = chunks[Random.Range(1, chunks.Length)];
        GameObject nextChunk = Instantiate(nextChunkPrefab, Vector2.zero, Quaternion.identity, transform);

        Transform startPoint = nextChunk.transform.Find("StartPoint");
        Transform endPoint = nextChunk.transform.Find("EndPoint");


        // Offset chunk so its StartPoint aligns with the last EndPoint
        Vector3 offset = lastEndPoint.position - startPoint.position;
        nextChunk.transform.position += offset;

        // Update for the next cycle
        lastEndPoint = endPoint;
    }

    private IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
