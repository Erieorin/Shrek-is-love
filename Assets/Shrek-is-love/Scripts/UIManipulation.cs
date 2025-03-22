using UnityEngine;

public class UIManipulation : MonoBehaviour 
{
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject DeathScreen;

    public void DeathSequence()
    {
        if (UICanvas != null && DeathScreen != null) 
        {
            UICanvas.SetActive(false);
            DeathScreen.SetActive(true);

            // Остановить время
            Time.timeScale = 0f;
        }
    }

    public void RestartSequence() 
    {
        if (UICanvas != null && DeathScreen != null) 
        {
            UICanvas.SetActive(true);
            DeathScreen.SetActive(false);

            // Восстановить время
            Time.timeScale = 1f;
        }
    }
}