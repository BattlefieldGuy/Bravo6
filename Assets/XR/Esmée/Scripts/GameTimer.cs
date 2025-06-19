using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 180f;
    private float midTime;
    private Heart heart;
    private float maxTime;

    [SerializeField] private GameObject attackersWin;
    [SerializeField] private GameObject defendersWin;

    [SerializeField] private GameObject lastMinute;

    [SerializeField] private Image bar;
    [SerializeField] private Image bar2;

    void Start()
    {
        heart = FindFirstObjectByType<Heart>();

        maxTime = endTimer;
        midTime = maxTime / 2;
    }

    void Update()
    {
        endTimer -= Time.deltaTime;

        if (endTimer <= 0f)
        {
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
        bool _noHealth = heart.CheckHealth();

        if (_noHealth)
        {
            attackersWin.SetActive(true);
            StartCoroutine(LoadStartScene());
        }

    }
    private void TimeIsUp()
    {
        bool _noHealth = heart.CheckHealth();

        if (_noHealth)
        {
            attackersWin.SetActive(true);
        }
        else
        {
            defendersWin.SetActive(true);
        }
        StartCoroutine(LoadStartScene());
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
