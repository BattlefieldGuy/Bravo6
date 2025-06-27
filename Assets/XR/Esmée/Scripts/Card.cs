using UnityEngine;

public class Card : MonoBehaviour
{
    [Range(1f, 20f)]
    public int CardCost;

    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private MinionScriptableObject costData;

    [SerializeField] private float spawnLine;


    private bool hasBeenPlayed = false;

    public void OnPlay(Vector3 _spawnPosition)
    {

        if (_spawnPosition.z < 4.3f || _spawnPosition.z > 6.2f)
        {

            if (gameObject.layer == LayerMask.NameToLayer("rotate"))
            {
                CoinManager.AddDECoins(costData.MCost);
            }
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                CoinManager.AddATCoins(costData.MCost);
            }

            hasBeenPlayed = false; //kijk hier naar
            Destroy(gameObject);
            return;
        }

        if (hasBeenPlayed) return;
        hasBeenPlayed = true;

        if (gameObject.layer == LayerMask.NameToLayer("rotate"))
        {
            if (_spawnPosition.z > spawnLine) { _spawnPosition.z = spawnLine; } //hi spawnt nu atijd op spawnlijn
        }
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            if (_spawnPosition.z < spawnLine) { _spawnPosition.z = spawnLine; }
        }


        if (_spawnPosition.x > -3.404f) _spawnPosition.x = -3.404f;
        if (_spawnPosition.x < -5.147f) _spawnPosition.x = -5.147f;

        {
            if (minionPrefab != null && minionPrefab != null && hasBeenPlayed)
            {
                _spawnPosition.y = 0.1f; //dit werkt goed zodra we echte minion  model met pivit beneden hebben
                if (gameObject.layer == LayerMask.NameToLayer("rotate"))
                {
                    Instantiate(minionPrefab, _spawnPosition, Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    Instantiate(minionPrefab, _spawnPosition, Quaternion.identity);
                }

            }
        }
    }
}



