using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public Slider healthSlider; // Ссылка на UI Slider

    private RectTransform healthBarRect; // RectTransform для изменения ширины
    
    private float baseWidth;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthBarRect = healthSlider.GetComponent<RectTransform>();
        baseWidth = healthBarRect.sizeDelta.x;
        Debug.Log("Health initialized: " + currentHealth);
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();
        Debug.Log("Damage taken! Current health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthUI();
        Debug.Log("Healing! Current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
        Application.Quit();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // Обновление шкалы здоровья при прокачке уровня
    public void UpgradeHealthBar(int newMaxHealth)
    {
        if (healthSlider != null && healthBarRect != null)
        {
            float widthMultiplier = (float)newMaxHealth / maxHealth; // Коэффициент, на который потом умножается ширина

            maxHealth = newMaxHealth;
            healthSlider.maxValue = maxHealth;
            currentHealth = maxHealth;

            float newWidth = baseWidth * widthMultiplier;
            healthBarRect.sizeDelta = new Vector2(newWidth, healthBarRect.sizeDelta.y);

            UpdateHealthUI();
            Debug.Log("Health upgrade! New health: " + currentHealth);
        }
    }
}