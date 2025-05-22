using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;
    [SerializeField] private float spawnLine;

    private bool hasBeenPlayed = false;

    public void OnPlay(Vector3 _spawnPosition)
    {
        if (hasBeenPlayed) return;
        hasBeenPlayed = true;

        if (_spawnPosition.z > spawnLine) _spawnPosition.z = spawnLine;
        if (_spawnPosition.x < -5.2f) _spawnPosition.x = -5.2f;
        if (_spawnPosition.x > -3.3f) _spawnPosition.x = -3.3f;

        if (_spawnPosition.z < 4.3f) _spawnPosition.z = 4.3f; //niet plaatsen
        {
            if (prefabData != null && prefabData.MPrefab != null)
            {
                _spawnPosition.y = 0.1f; //dit werkt goed zodra we echte minion  model met pivit beneden hebben
                Instantiate(prefabData.MPrefab, _spawnPosition, Quaternion.identity);
            }
        }
    }
}



