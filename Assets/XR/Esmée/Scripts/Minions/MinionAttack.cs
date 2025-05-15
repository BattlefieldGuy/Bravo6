using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    private float coolDown = 2;
    public MinionScriptableObject MDamageData;


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
                collision.gameObject.GetComponent<Wall>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }

        if (collision.collider.tag == "Heart")
        {
            if (coolDown <= 0)
            {
                Debug.Log("POOF");
                collision.gameObject.GetComponent<Heart>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }

        if (collision.collider.tag == "Tower")
        {
            if (coolDown <= 0)
            {
                Debug.Log("boing");
                collision.gameObject.GetComponent<Tower>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }
    }
}
