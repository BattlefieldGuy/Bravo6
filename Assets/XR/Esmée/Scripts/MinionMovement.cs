using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    private float speed = 1;
    [SerializeField] private int wallHealth = 100;
    [SerializeField] private Vector3 heart;

    void Update()
    {
        Walking();
    }

    private void Walking()
    {
        if (wallHealth <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, heart, Time.deltaTime * speed);
        }
        else
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
    }
}
