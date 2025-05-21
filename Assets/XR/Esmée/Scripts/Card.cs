using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;
    private bool hasBeenPlayed = false;

    public void OnPlay(Vector3 _spawnPosition)
    {
        if (hasBeenPlayed) return;
        hasBeenPlayed = true;


        if (Physics.Raycast(_spawnPosition, Vector3.down, out RaycastHit _hit))
        {
            Vector3 _hitPos = _spawnPosition;

            if (_hit.transform.CompareTag("PlayArea"))
            {
                if (prefabData != null && prefabData.MPrefab != null)
                {
                    Instantiate(prefabData.MPrefab, _spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("mowmow");
            }

        }
    }
}



