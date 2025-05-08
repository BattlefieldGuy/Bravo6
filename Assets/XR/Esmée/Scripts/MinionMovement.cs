using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    private float speed = 1;
    [SerializeField] private Vector3 heart;
    private Wall wHealth;

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
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, heart, Time.deltaTime * speed);
        }
    }
}
