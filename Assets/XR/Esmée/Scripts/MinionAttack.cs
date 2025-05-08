using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    public int Damage = 15;
    private float coolDown = 2;

    void Update()
    {
        coolDown -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Damageable")
        {
            if (coolDown <= 0)
            {
                Debug.Log("hit!");
                collision.gameObject.GetComponent<Wall>().TakeDamage(Damage);
                coolDown = 2;
            }
        }

        if (collision.collider.tag == "Heart")
        {
            if (coolDown <= 0)
            {
                Debug.Log("POOF");
                collision.gameObject.GetComponent<Heart>().TakeDamage(Damage);
                coolDown = 2;
            }
        }
    }
}
