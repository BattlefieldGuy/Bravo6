using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minion;
    void Start()
    {
        GameObject _minion = Instantiate(minion);
        _minion.transform.position = gameObject.transform.position;
    }
}