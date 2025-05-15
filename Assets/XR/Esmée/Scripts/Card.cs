using UnityEngine;


public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;

    public void OnPlay(Vector3 _spawnPosition)
    {
        if (prefabData != null && prefabData.MPrefab != null)
        {//hier zou je een minionkaart tag bij kunnen zetten als je meer dingen met vinger wilt spawnen
            Instantiate(prefabData.MPrefab, _spawnPosition, Quaternion.identity);
        }
    }

}
