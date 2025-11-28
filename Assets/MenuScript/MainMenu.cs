using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTetris()
    {
        SceneManager.LoadScene("Tetris");
    }

    public void LoadPong()
    {
        SceneManager.LoadScene("Pong");
    }

    public void LoadShootEmUp()
    {
        SceneManager.LoadScene("Shoot-Em Up");
    }

    public void LoadDuckHunt()
    {
        SceneManager.LoadScene("DuckHunt");
    }

    public void LoadPacMan()
    {
        SceneManager.LoadScene("Pac-Man");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

