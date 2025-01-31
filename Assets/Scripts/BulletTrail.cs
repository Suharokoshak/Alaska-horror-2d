using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
