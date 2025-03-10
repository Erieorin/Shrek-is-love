using UnityEngine;

// Для тестов. Лежит на Player, можно отключить
public class Player : MonoBehaviour
{
    public HealthSystem healthSystem;
    public ManaSystem manaSystem;

    void Start()
    {
        // Пример вызова UpgradeHealthBar через 5 секунд
        Invoke("UpgradeHealth", 5f);
        Invoke("UpgradeMana", 10f);
    }

    void UpgradeHealth()
    {
        if (healthSystem != null)
        {
            healthSystem.UpgradeHealthBar(40); // Увеличиваем максимальное здоровье до 40
        }
    }

    void UpgradeMana()
    {
        if (healthSystem != null)
        {
            manaSystem.UpgradeManaBar(30); // Увеличиваем максимальное здоровье до 40
        }
    }
}