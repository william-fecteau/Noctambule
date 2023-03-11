using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSpawnZone : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private GameObject mothPrefab;
    [SerializeField] private float minTimeBeforeSpawnSec = 5.0f;
    [SerializeField] private float maxTimeBeforeSpawnSec = 10.0f;
    [SerializeField] private int spawnCap = 10;

    private float timerNextSpawn;
    private float timeBeforeNextSpawnSec;
    private float mothCount; // TODO: This will probably need a refactor when we will be able to capture moth manually or automatically

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (mothCount > spawnCap) return;

        if (timerNextSpawn > timeBeforeNextSpawnSec)
        {
            SpawnMoth();
            DecideNextSpawnTime();
        }

        timerNextSpawn += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BaseMoth>(out BaseMoth moth))
        {
            mothCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BaseMoth>(out BaseMoth moth))
        {
            mothCount--;
        }
    }


    private void SpawnMoth()
    {
        float minX = boxCollider.bounds.min.x;
        float maxX = boxCollider.bounds.max.x;
        float minY = boxCollider.bounds.min.y;
        float maxY = boxCollider.bounds.max.y;

        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        Instantiate(mothPrefab, spawnPosition, Quaternion.identity);

        mothCount++;
    }

    private void DecideNextSpawnTime()
    {
        timeBeforeNextSpawnSec = Random.Range(minTimeBeforeSpawnSec, maxTimeBeforeSpawnSec);
        timerNextSpawn = 0.0f;
    }
}
