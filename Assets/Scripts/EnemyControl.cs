using UnityEditor;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthSystem healthSys = collision.gameObject.GetComponent<HealthSystem>();
        if (healthSys != null)
        {
            healthSys.TakeDamage(22);
        }
        
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
