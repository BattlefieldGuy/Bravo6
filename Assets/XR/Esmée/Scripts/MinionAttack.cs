using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    private int damage = 15;
    private float coolDown = 2;
    void Start()
    {

    }

    void Update()
    {
        coolDown -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "meow")
        {
            if (coolDown <= 0)
            {
                Debug.Log("hit!");
                coolDown = 2;
            }
        }
    }
}
