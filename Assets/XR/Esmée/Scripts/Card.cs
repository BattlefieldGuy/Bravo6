using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;
    private bool hasBeenPlayed = false;

    public void OnPlay(Vector3 _spawnPosition)
    {
        if (hasBeenPlayed) return;
        hasBeenPlayed = true;

        if (prefabData != null && prefabData.MPrefab != null)
        {
            Instantiate(prefabData.MPrefab, _spawnPosition, Quaternion.identity);
        }
    }
}



