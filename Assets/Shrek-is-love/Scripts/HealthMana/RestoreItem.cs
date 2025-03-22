using UnityEngine;

public class RestoreItem : MonoBehaviour
{
    [SerializeField] private int restoreAmount = 5;
    [SerializeField] private bool restoreHealth = true; 

    private void OnTriggerEnter(Collider other)
    {
        if (restoreHealth)
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Heal(restoreAmount);
            }
        }
        else
        {
            ManaSystem manaSystem = other.GetComponent<ManaSystem>();
            if (manaSystem != null)
            {
                manaSystem.RestoreMana(restoreAmount);
            }
        }
    }
}