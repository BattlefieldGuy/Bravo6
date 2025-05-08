using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    private float mHealth = 100;

    public void TakeDamage(float _damageM)
    {
        mHealth -= _damageM;
    }
}
