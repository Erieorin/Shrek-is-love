using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 2;

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damageAmount); // Наносим урон объекту, который вошел в триггер
        }
    }
}