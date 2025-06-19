using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private Wall wHealth;

    public MinionScriptableObject MSpeedData;

    public bool IsWalking = true;



    private void Start()
    {
        wHealth = FindFirstObjectByType<Wall>();
    }
    void Update()
    {
        if (IsWalking)
            Walking();
    }

    private void Walking()
    {
        if (wHealth != null)
        {
            if (wHealth.WallHealth >= 0f)
            {
                transform.position += MSpeedData.MSpeed * Time.deltaTime * Vector3.forward;
            }
        }
        else
        {
            if (heart != null)
                transform.position = Vector3.MoveTowards(transform.position, heart.transform.position, Time.deltaTime * MSpeedData.MSpeed);
        }
    }
}
