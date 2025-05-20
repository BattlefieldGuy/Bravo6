using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float endTimer = 300f;
    void Start()
    {

    }

    void Update()
    {
        endTimer -= Time.deltaTime;

        if (endTimer <= 0f)
        {
            TimeIsUp();
        }
    }


    private void TimeIsUp()
    {
        Debug.Log("times up bbg");
        //win/lose screen

    }
}
