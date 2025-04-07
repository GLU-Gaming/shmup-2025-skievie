using UnityEngine;
using UnityEngine.SceneManagement;

public class Utlilities : MonoBehaviour
{
    public void GoToEndGame()
    {
        SceneManager.LoadScene("EndGameScreen");
    }

    public void GoToPayamTestGame()
    {
        SceneManager.LoadScene("DevTestScene");
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void GoToMichaelTestGame()
    {
        SceneManager.LoadScene("MichaelDevSceneTest");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void MadeByScene()
    {
        SceneManager.LoadScene("MadeBy");
    }
}
