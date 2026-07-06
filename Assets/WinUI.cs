using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        SceneManager.LoadScene("MenuScene");
#else
        Application.Quit();
#endif
    }
}