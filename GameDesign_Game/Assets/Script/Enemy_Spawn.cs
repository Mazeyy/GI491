using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    [SerializeField]
    public float spawnRadius = 4, spawntime = 1.5f;

    public GameObject[] enemies;
    
    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }

    IEnumerator SpawnAnEnemy()
    {
        if (Player_Stats.PlayerStats.Health > 0)
        {
            Vector2 spawnPosition = GameObject.Find("Player").transform.position;
            spawnPosition += Random.insideUnitCircle.normalized * spawnRadius;

            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawntime);
            StartCoroutine(SpawnAnEnemy());
        }
    }
    
}
