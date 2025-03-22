using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour 
{
    public int maxMana = 15;
    private int currentMana;

    public Slider manaSlider; // Ссылка на UI Slider
    public Image manaImage;   // Ссылка на Image для спрайтов
    public Sprite mana100;   // Спрайт для 100% маны
    public Sprite mana75;    // Спрайт для 75% маны
    public Sprite mana50;    // Спрайт для 50% маны
    public Sprite mana25;    // Спрайт для 25% маны
    public Sprite mana0;     // Спрайт для 0% маны

    private RectTransform manaBarRect; // RectTransform для изменения ширины
    
    private float baseWidth;

    // В юнити для создания Singleton объекта делают так обычно
    // private static ManaSystem _instance;
    // а потом в методе Awake можно написать так    
    //
    // void Awake() 
    // {
    //     if (_instance == null) 
    //     {
    //         _instance = FindObjectOfType<ManaSystem>();
    //         if (_instance == null) 
    //         {
    //             _instance == this;
    //         }
            
    //     } 
        
    //     if (_instance != this)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
        
    // }

    void Start()
    {
        manaSlider.maxValue = maxMana;
        currentMana = maxMana;
        manaBarRect = manaSlider.GetComponent<RectTransform>();
        baseWidth = manaBarRect.sizeDelta.x;
        Debug.Log("Mana initialized: " + currentMana);
        UpdateManaUI();
    }

    public bool UseSufficientMana(int usedMana)
    {
        if (currentMana - usedMana >= 0) 
        {
            currentMana -= usedMana;
            currentMana = Mathf.Max(currentMana, 0);
            UpdateManaUI();
            Debug.Log("Mana used! Current mana: " + currentMana);
            return true;
        } 
        else 
        {
            Debug.Log("Not sufficient mana! Current mana: " + currentMana);
            return false;
        }
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Min(currentMana, maxMana);
        UpdateManaUI();
        Debug.Log("Mana restored! Current mana: " + currentMana);
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }

    private void UpdateManaUI()
    {
        if (manaSlider != null)
        {
            manaSlider.value = currentMana;
        }

        // Обновляем спрайт в зависимости от процента маны
        if (manaImage != null)
        {
            float manaPercent = (float)currentMana / maxMana * 100f;

            if (manaPercent > 75f)
            {
                manaImage.sprite = mana100;
            }
            else if (manaPercent > 50f)
            {
                manaImage.sprite = mana75;
            }
            else if (manaPercent > 25f)
            {
                manaImage.sprite = mana50;
            }
            else if (manaPercent > 0f)
            {
                manaImage.sprite = mana25;
            }
            else
            {
                manaImage.sprite = mana0;
            }
        }
    }

    // Обновление шкалы маны при прокачке уровня
    public void UpgradeManaBar(int newMaxMana)
    {
        if (manaSlider != null && manaBarRect != null)
        {
            float widthMultiplier = (float)newMaxMana / maxMana; // Коэффициент, на который потом умножается ширина

            maxMana = newMaxMana;
            manaSlider.maxValue = maxMana;
            currentMana = maxMana;

            float newWidth = baseWidth * widthMultiplier;
            manaBarRect.sizeDelta = new Vector2(newWidth, manaBarRect.sizeDelta.y);

            UpdateManaUI();
            Debug.Log("Mana upgrade! New mana: " + currentMana);
        }
    }
}