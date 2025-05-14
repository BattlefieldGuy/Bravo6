using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private Vector3 heart;
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
        if (wHealth.WallHealth >= 0.01f)
        {
            transform.position += Vector3.forward * Time.deltaTime * MSpeedData.MSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, heart, Time.deltaTime * MSpeedData.MSpeed);
        }
    }
}
