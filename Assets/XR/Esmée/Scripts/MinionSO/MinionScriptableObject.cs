using UnityEngine;

[CreateAssetMenu(fileName = "MinionScriptableObject", menuName = "Scriptable Objects/MinionScriptableObject")]
public class MinionScriptableObject : ScriptableObject
{
    public float MSpeed;
    public float MDamage;
    public float MHealth;
    //audioclip zou je hier ook kunnen doen hier. als je ander geluid per minion wil
    //wapen info kan ook hier
    //range kan ook??
    //moet hier ook de minion prefab?
}
