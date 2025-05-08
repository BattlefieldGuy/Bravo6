using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    [SerializeField] private float mHealth = 100;
    private void Update()
    {
        MDeath();
    }

    public void TakeDamage(float _damageM)
    {
        mHealth -= _damageM;
    }

    private void MDeath()
    {
        if (mHealth <= 0)
        {
            Destroy(gameObject);
            //animatie hier anders word Rik boos;
        }
    }
}
