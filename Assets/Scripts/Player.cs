using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject flash;
    [SerializeField] private float destroyShootTime;
    [SerializeField] private BulletTrail trail;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _directionLook;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<HealthSystem>().OnDeath.AddListener(OnDeathPlayer);

    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _movementInput * speed;
    }

    void Update()
    {
        GetMovementInput();
        RotateCharacter();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.transform.position, _directionLook, 10000);
            GameObject flashCopy = Instantiate(flash, shootPoint.transform);
            BulletTrail trailCopy = Instantiate(trail, shootPoint.transform.position, shootPoint.transform.rotation);
            trailCopy.SetDirection(_directionLook);

            Destroy(flashCopy, destroyShootTime);

            if (hit.collider != null)
            {
                HealthSystem healthSys = hit.collider.gameObject.GetComponent<HealthSystem>();
                if (healthSys != null)
                {
                    healthSys.TakeDamage(42);

                }
            }
        }
    }

    private void RotateCharacter()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _directionLook = (mousePos - transform.position).normalized;

        float targetAngle = Mathf.Atan2(_directionLook.y, _directionLook.x) * Mathf.Rad2Deg;
        float currentAngle = transform.rotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
    }

    private void GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementInput = new Vector2(horizontal, vertical);
    }
    private int TextHello(string textDarova)
    {


        Debug.Log(textDarova);
        return 2;



    }
    private void OnDeathPlayer()
    {
        Destroy(gameObject);


    }


}