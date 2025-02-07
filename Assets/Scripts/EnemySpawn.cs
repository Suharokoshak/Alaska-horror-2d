using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyControl[] zombiePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerRespawner playerRespawner;

    private void Awake()
    {
        playerRespawner.OnPlayerSpawnEvent += (Transform player) =>
        {
            this.player = player;
        };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    private void EnemySpawner()
    {
        int pointIndex = Random.Range(0, spawnPoints.Length);
        Transform randomPoint = spawnPoints[pointIndex];
        int zombieIndex = Random.Range(0, zombiePrefab.Length);
        EnemyControl randomZombie = zombiePrefab[zombieIndex];

        EnemyControl zombie = Instantiate(randomZombie, randomPoint.position, Quaternion.identity);
        zombie.SetPlayerModel(player);
        zombie.SetPlayerRespawner(playerRespawner);
    }

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i<5; i++)
            {
                EnemySpawner();
            }
        } 

    }
}
