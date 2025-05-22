using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    private float mHealth = 100;

    private MinionScriptableObject MLevelData;
    private MinionScriptableObject MCostData;

    private void Start()
    {
    }
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
            CoinManager.GainMinionPrize(MLevelData.MLevel, MCostData.MCost);
            Destroy(gameObject);
        }
    }
}
