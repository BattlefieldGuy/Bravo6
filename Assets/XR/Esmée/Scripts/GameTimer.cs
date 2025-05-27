using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 300f; //5 minuten, 3m=180f
    private float midTime;
    private Heart heart;

    [SerializeField] private GameObject attackersWin;
    [SerializeField] private GameObject defendersWin;

    [SerializeField] private GameObject lastMinute;
    [SerializeField] private GameObject timer1;
    [SerializeField] private GameObject timer2;

    private TMPro.TextMeshProUGUI timerText1;
    private TMPro.TextMeshProUGUI timerText2;
    void Start()
    {
        heart = FindFirstObjectByType<Heart>();

        midTime = 60;

        timerText1 = timer1.GetComponent<TMPro.TextMeshProUGUI>();
        timerText2 = timer2.GetComponent<TMPro.TextMeshProUGUI>();
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
        LoadStartScene();
    }

    private void LastMinute()
    {
        if (lastMinute != null)
        {
            lastMinute.SetActive(true);
            timer1.SetActive(true);
            timer2.SetActive(true);
            StartCoroutine(enumerator());
        }

        int _time = ((int)endTimer);

        timerText1.text = _time.ToString();
        timerText2.text = _time.ToString();
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5f);
        Destroy(lastMinute);
    }

    private void LoadStartScene()
    {
        Destroy(timerText1);
        Destroy(timerText2);

        if (endTimer <= -5)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
