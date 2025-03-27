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
        SceneManager.LoadScene("EndGameScreen");
    }

    public void GoToMichaelTestGame()
    {
        SceneManager.LoadScene("MichaelDevSceneTest");
    }
}
