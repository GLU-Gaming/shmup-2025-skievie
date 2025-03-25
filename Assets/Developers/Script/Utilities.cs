using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    
    public void GoToGame()
    {
        SceneManager.LoadScene("DevTestScene");
    }

    public void GoToStart()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void GoToEndScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }


}
