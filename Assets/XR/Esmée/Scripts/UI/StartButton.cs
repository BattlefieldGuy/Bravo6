using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }



    public void Credits()
    {
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}


