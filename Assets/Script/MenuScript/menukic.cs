using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Hàm này sẽ được gọi khi nhấn nút để tải scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2(string sceneName)
    {
        SceneManager.LoadScene(0);
    }

    // Hàm này sẽ được gọi khi nhấn nút để thoát game
    public void QuitGame()
    {
       Application.Quit();
    }
}
