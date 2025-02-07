using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Transform playerModel;
    private Rigidbody2D rigidBody2D;
    private PlayerRespawner playerRespawner;

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
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidBody2D.linearVelocity = direction * 1;
    }

    private void Update()
    {
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
