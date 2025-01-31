using UnityEditor;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject playerModel;
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSys) && collision.gameObject.TryGetComponent(out EnemyControl _) == false)
        {
            healthSys.TakeDamage(22);
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        playerModel = FindFirstObjectByType<Player>().gameObject;
        Vector2 direction = playerModel.transform.position - gameObject.transform.position;
        rigidBody2D.linearVelocity = direction * speed;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        float currentAngle = transform.rotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);



    }
    private void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();


    }
}
