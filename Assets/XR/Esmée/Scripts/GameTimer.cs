using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 300f; //5 minuten, 3m=180f
    private Heart heart;

    void Start()
    {
        heart = FindFirstObjectByType<Heart>();
    }

    void Update()
    {
        endTimer -= Time.deltaTime;

        if (endTimer <= 0f)
        {
            TimeIsUp();
        }
        //je zou kunnen doen van als timer op de helft is geef reminder. maar moeten even kijken qua art
    }


    private void TimeIsUp()
    {
        bool _noHealth = heart.CheckHealth();

        if (_noHealth)
        {
            Debug.Log("attacker wint");
        }
        else
        {
            Debug.Log("defender wint");
        }
    }
}
