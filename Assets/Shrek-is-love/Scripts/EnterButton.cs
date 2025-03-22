using UnityEngine;
using UnityEngine.UI;

public class EnterButton : MonoBehaviour
{
    [SerializeField] private Button button; // Ссылка на кнопку

    private void Update()
    {
        // Проверяем нажатие Enter (основной или на цифровой клавиатуре)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Активируем кнопку
            button.onClick.Invoke();
        }
    }
}