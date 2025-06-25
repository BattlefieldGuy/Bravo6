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
                if (gameObject.layer == LayerMask.NameToLayer("enemyL"))
                {
                    transform.position += MSpeedData.MSpeed * Time.deltaTime * Vector3.forward; // l to r
                }
                if (gameObject.layer == LayerMask.NameToLayer("enemyR"))
                {
                    transform.position += MSpeedData.MSpeed * Time.deltaTime * Vector3.back; // r to l
                }
            }
        }
        else
        {
            if (heart != null)
                transform.position = Vector3.MoveTowards(transform.position, heart.transform.position, Time.deltaTime * MSpeedData.MSpeed);
        }
    }
}
