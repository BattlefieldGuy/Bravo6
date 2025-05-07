using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private bool isPlaced;
    private float speed = 1;

    void Start()
    {

    }

    void Update()
    {
        Walking();
    }

    private void Walking()
    {
        if (isPlaced)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;

        }
    }
}
