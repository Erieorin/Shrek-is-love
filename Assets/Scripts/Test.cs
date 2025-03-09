using UnityEngine;

// Для тестов. Лежит на Player, можно отключить
public class Player : MonoBehaviour
{
    public HealthSystem healthSystem;

    void Start()
    {
        // Пример вызова UpgradeHealthBar через 5 секунд
        Invoke("UpgradeHealth", 5f);
    }

    void UpgradeHealth()
    {
        if (healthSystem != null)
        {
            healthSystem.UpgradeHealthBar(40); // Увеличиваем максимальное здоровье до 40
        }
    }
}