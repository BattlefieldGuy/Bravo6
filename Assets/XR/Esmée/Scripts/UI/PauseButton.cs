using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pause;

    public void PauseGame()
    {
        menu.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        pause.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
