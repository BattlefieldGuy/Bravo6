using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 300f; //5 minuten, 3m=180f
    private float midTime;
    private Heart heart;

    [SerializeField] private GameObject attackersWin;
    [SerializeField] private GameObject defendersWin;

    [SerializeField] private GameObject halfTime;
    [SerializeField] private GameObject timer;

    void Start()
    {
        heart = FindFirstObjectByType<Heart>();

        midTime = endTimer / 2;
    }

    void Update()
    {
        endTimer -= Time.deltaTime;

        if (endTimer <= 0f)
        {
            TimeIsUp();
        }
        else if (endTimer <= midTime)
        {
            HalfTime();
        }
        //je zou kunnen doen van als timer op de helft is geef reminder. maar moeten even kijken qua art
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
    }

    private void HalfTime()
    {
        if (halfTime != null)
        {
            halfTime.SetActive(true);
            timer.SetActive(true);
        }

    }
}
