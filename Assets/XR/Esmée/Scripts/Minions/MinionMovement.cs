using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private Wall wHealth;

    public MinionScriptableObject MSpeedData;



    private void Start()
    {
        wHealth = FindFirstObjectByType<Wall>();
    }
    void Update()
    {
        Walking();
    }

    private void Walking()
    {
        if (wHealth != null)
        {
            if (wHealth.WallHealth >= 0f)
            {
                transform.position += Vector3.forward * Time.deltaTime * MSpeedData.MSpeed;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, heart.transform.position, Time.deltaTime * MSpeedData.MSpeed);
        }
    }
}
