using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public UIManipulation UIManipulation;

    // Метод для перезапуска уровня
    public void Restart()
    {
        UIManipulation.RestartSequence();
        // Получить имя текущей сцены и перезагрузить её
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}