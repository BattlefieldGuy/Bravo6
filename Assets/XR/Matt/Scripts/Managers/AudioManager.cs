using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio clips")]
    [SerializeField] private AudioClip battleComencing;


    private AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    void Start()
    {
        src.PlayOneShot(battleComencing);
    }

}