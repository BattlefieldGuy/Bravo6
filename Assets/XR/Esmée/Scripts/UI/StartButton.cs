using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject creds;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void Credits()
    {
        creds.SetActive(true);
    }

    public void CloseCredits()
    {
        creds.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}


