using UnityEngine;

public class ActivatePS : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;

    public void ActivatePSEffect()
    {
        if (particleSystem != null)
            particleSystem.Play();
    }
}
