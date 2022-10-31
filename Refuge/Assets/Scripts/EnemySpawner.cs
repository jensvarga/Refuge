using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Pool;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private Transform[] spawnPoints;

    private int spawnerIndex = 0;
    private bool spawned = false;
    
    #region Server
    
    private void Start() {
        if (!isServer) { return; }
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (!isServer) { return; }
        Transform trans = RoundRobinSpawnLocation();
        GameObject enemyInstance = Instantiate(enemyPrefab, trans.position, trans.rotation);
        Debug.Log($"GROBLN at {trans.position}");
        NetworkServer.Spawn(enemyInstance);
    }

    private Transform RoundRobinSpawnLocation()
    {
        Transform trans = null;

        if (spawnerIndex < spawnPoints.Length - 1) {
            trans = spawnPoints[spawnerIndex];
            spawnerIndex++;
        } else {
            spawnerIndex = 0;
            trans = spawnPoints[spawnerIndex];
        }

        return trans;
    }

    #endregion

    #region Client

    #endregion
    
}
