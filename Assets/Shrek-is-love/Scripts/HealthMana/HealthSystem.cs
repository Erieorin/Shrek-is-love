using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    [SerializeField] private Slider healthSlider; // Ссылка на UI Slider
    [SerializeField] private Image healthImage;   // Ссылка на Image для спрайтов
    [SerializeField] private Sprite health100;   // Спрайт для 100% здоровья
    [SerializeField] private Sprite health75;    // Спрайт для 75% здоровья
    [SerializeField] private Sprite health50;    // Спрайт для 50% здоровья
    [SerializeField] private Sprite health25;    // Спрайт для 25% здоровья
    [SerializeField] private Sprite health0;     // Спрайт для 0% здоровья

    private RectTransform healthBarRect; // RectTransform для изменения ширины
    
    private float baseWidth;

    [SerializeField] private UIManipulation uiManipulation;

    private static HealthSystem _instance;

    public static HealthSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HealthSystem>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "HealthSystem";
                    _instance = obj.AddComponent<HealthSystem>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        uiManipulation.DeathSequence();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Обновляем спрайт в зависимости от процента здоровья
        if (healthImage != null)
        {
            float healthPercent = (float)currentHealth / maxHealth * 100f;

            if (healthPercent > 75f)
            {
                healthImage.sprite = health100;
            }
            else if (healthPercent > 50f)
            {
                healthImage.sprite = health75;
            }
            else if (healthPercent > 25f)
            {
                healthImage.sprite = health50;
            }
            else if (healthPercent > 0f)
            {
                healthImage.sprite = health25;
            }
            else
            {
                healthImage.sprite = health0;
            }
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