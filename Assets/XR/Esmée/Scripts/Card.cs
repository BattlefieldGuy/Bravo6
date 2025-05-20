using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;
    private bool hasBeenPlayed = false;

    [SerializeField] private float spawnLineFront;
    [SerializeField] private float spawnLineLeft; //ugly
    [SerializeField] private float spawnLineRight;
    [SerializeField] private float spawnLineBack;

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



