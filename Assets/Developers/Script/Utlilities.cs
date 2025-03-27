using UnityEngine;
using UnityEngine.SceneManagement;

public class Utlilities : MonoBehaviour
{
    public void GoToEndGame()
    {
        SceneManager.LoadScene("EndGameScreen");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("DevTestScene");
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene("EndGameScreen");
    }
}
