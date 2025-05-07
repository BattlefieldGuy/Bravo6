using UnityEngine;

public class Heart : MonoBehaviour
{
    private float healt = 100;

    public void TakeDamage(float _damage)
    {
        healt -= _damage;
        CheckHealt();
    }

    public void CheckHealt()
    {
        if (healt < 0.0001)
        {
            Debug.LogError("DEAD");
            Destroy(gameObject);
        }
    }
}