using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    public MinionScriptableObject MHealthData;
    private void Update()
    {
        MDeath();
        Debug.Log(MHealthData.MHealth);
    }

    public void TakeDamage(float _damageM)
    {
        MHealthData.MHealth -= _damageM;
    }

    private void MDeath()
    {
        if (MHealthData.MHealth <= 0)
        {
            Destroy(gameObject);
            //animatie hier anders word Rik boos;
        }
    }
}
