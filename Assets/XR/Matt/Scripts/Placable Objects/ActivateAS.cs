using UnityEngine;

public class ActivateAS : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public void ActivateASEffect()
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}