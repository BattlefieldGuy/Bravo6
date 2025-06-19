using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject creds;
    [SerializeField] private GameObject blockPlay;

    private bool isPressed1;
    private bool isPressed2;

    private void Update()
    {
        GetReady();
    }
    private void GetReady()
    {
        if (isPressed1 && isPressed2)
        {
            blockPlay.SetActive(false);
        }
    }
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


    public void Player1Ready()
    {
        isPressed1 = true;
    }

    public void Player2Ready()
    {
        isPressed2 = true;
    }
}


