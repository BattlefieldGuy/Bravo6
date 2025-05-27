using UnityEngine;

[CreateAssetMenu(fileName = "MinionScriptableObject", menuName = "Scriptable Objects/MinionScriptableObject")]
public class MinionScriptableObject : ScriptableObject
{
    public float MSpeed;
    public float MDamage;
    public float MCost;
    public int MLevel;
    public int MPrize;
    public GameObject MPrefab;
    //audioclip zou je hier ook kunnen doen hier. als je ander geluid per minion wil. voor bv lopen en slaan
    //wapen info kan ook hier
    //range kan ook??
}
