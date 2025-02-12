using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public UnityEvent OnDeath = new UnityEvent();
    [SerializeField] private float health;
     private bool isDead = false;
    public void TakeDamage(float damage)
    {
        if (health > 0 && isDead == false)
        {
            health = health - damage;
            Debug.Log("Health =  " + health);

            if (health <= 0 )
            {
                isDead = true;
                OnDeath.Invoke();
            }
        }
    }

}
