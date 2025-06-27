using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MinionHealth : MonoBehaviour
{
    private float mHealth;
    private float maxHealth;
    [SerializeField] private Image bar;

    [SerializeField] private ParticleSystem particleSyst;
    [SerializeField] private GameObject minion;

    [SerializeField] private AudioClip deathAudio;

    public MinionScriptableObject MLevelData;
    public MinionScriptableObject MPrizeData;
    public MinionScriptableObject MHealthData;

    private bool hasPlayed = false;


    private void Start()
    {
        mHealth = MHealthData.MHealth;
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
            if (!hasPlayed)
            {
                this.GetComponent<AudioSource>().PlayOneShot(deathAudio);
                GetComponent<BoxCollider>().enabled = false;
                particleSyst.Play();
                Destroy(minion);
                StartCoroutine(WaitForSec());
                hasPlayed = true;
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.layer == LayerMask.NameToLayer("enemyR"))
        {
            CoinManager.GainMinionPrize(MLevelData.MLevel, MPrizeData.MPrize);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("enemyL"))
        {
            CoinManager.GainTowerPrize(MLevelData.MLevel, MPrizeData.MPrize);
        }
        Destroy(gameObject);
    }
}
