using UnityEngine;

public class Heart : MonoBehaviour
{
    private float health = 100;

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if (CheckHealth())
            Destroy(gameObject);
    }

    public bool CheckHealth()
    {
        if (health < 0.0001)
        {
            Debug.LogError("DEAD");
            return true;
        }
        else
            return false;
    }
}