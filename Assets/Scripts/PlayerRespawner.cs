using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawner : MonoBehaviour
{
    [SerializeField]private GameObject[] points;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineCamera cinemachineCam;

    public UnityAction<Transform> OnPlayerSpawnEvent;
    
    public void OnRespawn()
    {
        int pointIndex = Random.Range(0, points.Length);  
        GameObject randomPoint = points[pointIndex];
        GameObject playerSpawned = Instantiate(playerPrefab, randomPoint.transform.position, Quaternion.identity);
        CameraTarget target = new CameraTarget();
        target.TrackingTarget = playerSpawned.transform;
        cinemachineCam.Target = target;
        playerSpawned.GetComponent<HealthSystem>().OnDeath.AddListener(OnRespawn);
        OnPlayerSpawnEvent.Invoke(playerSpawned.transform);
    }
}
