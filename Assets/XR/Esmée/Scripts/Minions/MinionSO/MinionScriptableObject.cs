using UnityEngine;

[CreateAssetMenu(fileName = "MinionScriptableObject", menuName = "Scriptable Objects/MinionScriptableObject")]
public class MinionScriptableObject : ScriptableObject
{
    public float MSpeed;
    public float MDamage;
    public int MCost;
    public int MLevel;
    public int MPrize;
    public GameObject MPrefab;
    public AudioClip MAttackAudio;

    //wapen info kan ook hier
    //range kan ook??
}
