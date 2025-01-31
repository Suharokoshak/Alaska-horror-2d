using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i = 0;i < 3; i++)
        {
            EnemySpawner();
        }
    }

   
   private void EnemySpawner()
    {
        int pointIndex = Random.Range(0, spawnPoints.Length);
        GameObject randomPoint = spawnPoints[pointIndex];
        GameObject zimboSpawned = Instantiate(zombiePrefab, randomPoint.transform.position, Quaternion.identity);


    }
}
