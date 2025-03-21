using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healAmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.Heal(healAmount);
        }
    }
}