using UnityEngine;

public class Card : MonoBehaviour
{
    [Range(1f, 20f)]
    public int CardCost;

    [SerializeField] private MinionScriptableObject prefabData;
    [SerializeField] private MinionScriptableObject costData;

    [SerializeField] private float spawnLine;

    private bool hasBeenPlayed = false;

    public void OnPlay(Vector3 _spawnPosition)
    {

        if (_spawnPosition.z < 4.3f)
        {
            CoinManager.AddATCoins(costData.MCost);
            hasBeenPlayed = false; //kijk hier naar
            Destroy(gameObject);
            return;
        }

        if (hasBeenPlayed) return;
        hasBeenPlayed = true;

        if (_spawnPosition.z > spawnLine) _spawnPosition.z = spawnLine;
        if (_spawnPosition.x < -5.2f) _spawnPosition.x = -5.2f;
        if (_spawnPosition.x > -3.3f) _spawnPosition.x = -3.3f;

        {
            if (prefabData != null && prefabData.MPrefab != null && hasBeenPlayed)
            {
                _spawnPosition.y = 0.1f; //dit werkt goed zodra we echte minion  model met pivit beneden hebben
                Instantiate(prefabData.MPrefab, _spawnPosition, Quaternion.identity);
            }
        }
    }
}



