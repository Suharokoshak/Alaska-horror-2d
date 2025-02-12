using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Transform playerModel;
    private PlayerRespawner playerRespawner;
    private NavMeshAgent navMeshAgent;

    private Vector2 direction;
    
    public void Death()
    {
        Destroy(gameObject);
    }

    public void SetPlayerRespawner(PlayerRespawner playerRespawner)
    {
        this.playerRespawner = playerRespawner;

        this.playerRespawner.OnPlayerSpawnEvent += (Transform player) =>
        {
            playerModel = player;
        };
    }

    public void SetPlayerModel(Transform playerModel)
    {
        this.playerModel = playerModel;
    }

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        
        navMeshAgent.speed = speed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        navMeshAgent.destination = playerModel.position;
        direction = (playerModel.position - gameObject.transform.position).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        float currentAngle = transform.rotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSys) && collision.gameObject.TryGetComponent(out EnemyControl _) == false)
        {
            healthSys.TakeDamage(22);
        }
    }
}
