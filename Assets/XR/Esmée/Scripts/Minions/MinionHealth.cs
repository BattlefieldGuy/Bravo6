using UnityEngine;
using UnityEngine.UI;
public class MinionHealth : MonoBehaviour
{
    private float mHealth = 100;
    private float maxHealth;
    [SerializeField] private Image bar;

    public MinionScriptableObject MLevelData;
    public MinionScriptableObject MPrizeData;

    private void Start()
    {
        maxHealth = mHealth;
    }
    private void Update()
    {
        MDeath();
        bar.fillAmount = Mathf.Clamp(mHealth / maxHealth, 0, 1);
    }

    public void TakeDamage(float _damageM)
    {
        mHealth -= _damageM;
    }

    private void MDeath()
    {
        if (mHealth <= 0)
        {
            CoinManager.GainMinionPrize(MLevelData.MLevel, MPrizeData.MPrize);
            Destroy(gameObject);
        }
    }
}
