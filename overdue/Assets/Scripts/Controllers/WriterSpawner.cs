using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriterSpawner : MonoBehaviour
{
    [SerializeField]
    public static bool IsDead = true;

    public GameObject writerPrefab;
    public Vector2[] spawnPoints;
    public float spawnCooldown;

    private float currentSpawnCooldown;

    private void Awake()
    {
        currentSpawnCooldown = 0;
    }
    private void Update()
    {
        foreach (var point in spawnPoints)
        {
            if (currentSpawnCooldown <= 0 && IsDead) {
                writerPrefab.transform.position = point;
                Instantiate(writerPrefab);

                currentSpawnCooldown = spawnCooldown;
                IsDead = false;
            }
            currentSpawnCooldown -= Time.deltaTime;
        }
    }

    public static void WriterGetsKilled() {
        IsDead = true;
    }
}
