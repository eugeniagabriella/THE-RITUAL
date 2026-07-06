using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
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
        // DI EDITOR: balik ke MenuScene
        SceneManager.LoadScene("MenuScene");
#else
        // DI BUILD: beneran keluar game
        Application.Quit();
#endif
    }
}