using UnityEngine;


public class Card : MonoBehaviour
{
    [SerializeField] private MinionScriptableObject prefabData;

    public void OnPlay()
    {
        if (prefabData != null && prefabData.MPrefab != null)
        {//hier zou je een minionkaart tag bij kunnen zetten als je meer dingen met vinger wilt spawnen
            Instantiate(prefabData.MPrefab, transform.position, Quaternion.identity);
        }
    }

}
