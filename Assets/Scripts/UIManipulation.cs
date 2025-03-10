using UnityEngine;

public class UIManipulation : MonoBehaviour 
{
    public GameObject UICanvas;
    public GameObject DeathScreen;

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