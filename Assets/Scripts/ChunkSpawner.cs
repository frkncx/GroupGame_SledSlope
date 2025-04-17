using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [Header("Chunk Prefabs")]
    public GameObject[] chunks;
    public GameObject[] checkpointChunks;

    private Transform lastEndPoint;
    private GameObject lastChunkPrefab;

    private bool canSpawn = true;

    [Header("Spawning Settings")]
    public float checkThreshold = 0.1f;
    private float spawnCooldown = 0.001f;

    private int chunkCount = 0;
    private int nextCheckpointIn = 0;
    private void Start()
    {
        nextCheckpointIn = Random.Range(10, 16);
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
        lastChunkPrefab = chunks[0];
    }

    private void SpawnNextChunk()
    {
        GameObject nextChunkPrefab;

        chunkCount++;

        if (chunkCount >= nextCheckpointIn)
        {
            nextChunkPrefab = checkpointChunks[Random.Range(0, checkpointChunks.Length)];
            nextCheckpointIn = chunkCount + Random.Range(10, 16);
        }
        else
        {
            do
            {
                nextChunkPrefab = chunks[Random.Range(1, chunks.Length)];
            }
            while (nextChunkPrefab == lastChunkPrefab && chunks.Length > 1);
        }

        GameObject nextChunk = Instantiate(nextChunkPrefab, Vector2.zero, Quaternion.identity, transform);

        Transform startPoint = nextChunk.transform.Find("StartPoint");
        Transform endPoint = nextChunk.transform.Find("EndPoint");

        Vector3 offset = lastEndPoint.position - startPoint.position;
        nextChunk.transform.position += offset;

        lastEndPoint = endPoint;
        lastChunkPrefab = nextChunkPrefab;
    }

    private IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}