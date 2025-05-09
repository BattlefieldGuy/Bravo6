using UnityEngine;

public class MinionSpawnerDbug : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(m_Prefab, gameObject.transform);
        }
    }
}
