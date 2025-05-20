using UnityEngine;

public class Heart : MonoBehaviour
{
    private float healt = 100;

    public void TakeDamage(float _damage)
    {
        healt -= _damage;
        if (CheckHealt())
            Destroy(gameObject);
    }

    public bool CheckHealt()
    {
        if (healt < 0.0001)
        {
            Debug.LogError("DEAD");
            return true;
        }
        else
            return false;
    }
}