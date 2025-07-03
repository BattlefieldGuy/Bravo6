using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minion;
    void Start()
    {
        GameObject _minion = Instantiate(minion);
        if (!gameObject.CompareTag("Rotate"))
        {

            _minion.transform.position = gameObject.transform.position;
        }
        else if (gameObject.CompareTag("Rotate"))
        {
            Debug.Log("SpawnRotated");
            _minion.transform.rotation = new Quaternion(0, 180, 0, 1);
            _minion.transform.position = gameObject.transform.position;
        }
    }
}