using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 180f;
    private float midTime;
    [SerializeField] private Heart dHeart;
    [SerializeField] private Heart aHeart;
    [SerializeField] private Wall dWall;
    [SerializeField] private Wall aWall;

    private float maxTime;

    [SerializeField] private GameObject attackersWin;
    [SerializeField] private GameObject defendersWin;
    [SerializeField] private GameObject itsADraw;


    [SerializeField] private GameObject lastMinute;
    [SerializeField] private GameObject extraMinute;

    [SerializeField] private Image bar;
    [SerializeField] private Image bar2;

    private bool alreadyChecked = false;

    void Start()
    {
        maxTime = endTimer;
        midTime = maxTime / 2;
    }

    void Update()
    {
        endTimer -= Time.deltaTime;

        if (endTimer <= 0f)
        {
            GameDraw();
            TimeIsUp();
        }

        if (endTimer <= midTime)
        {
            LastMinute();
        }

        bar.fillAmount = Mathf.Clamp(endTimer / maxTime, 0, 1);
        bar2.fillAmount = Mathf.Clamp(endTimer / maxTime, 0, 1);

        AttackersWin();
    }

    private void AttackersWin()
    {
        bool _noHealthA = aHeart.CheckHealth();
        bool _noHealthD = dHeart.CheckHealth();

        if (_noHealthD)
        {
            attackersWin.SetActive(true);
            StartCoroutine(LoadStartScene());
        }
        else if (_noHealthA)
        {
            defendersWin.SetActive(true);
            StartCoroutine(LoadStartScene());
        }
    }
    private void TimeIsUp()
    {
        if (alreadyChecked == false)
        {
            bool _noHealthA = dWall.CheckHealth();
            bool _noHealthD = aWall.CheckHealth();

            if (!_noHealthD && !_noHealthA)
            {
                endTimer = 60f;
                extraMinute.SetActive(true);
            }
            alreadyChecked = true;
        }
    }

    private void GameDraw()
    {
        if (alreadyChecked == true)
        {
            itsADraw.SetActive(true);
            StartCoroutine(LoadStartScene());
        }
    }

    private void LastMinute()
    {
        if (lastMinute != null)
        {
            lastMinute.SetActive(true);
            StartCoroutine(enumerator());
        }
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5f);
        Destroy(lastMinute);
    }

    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
    }
}
