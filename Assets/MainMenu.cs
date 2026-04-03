using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Slavica Free 2022 Version");
    }

    public void QuitGame()
    {
        Debug.Log("Вихід з гри!");
        Application.Quit();
    }
}
