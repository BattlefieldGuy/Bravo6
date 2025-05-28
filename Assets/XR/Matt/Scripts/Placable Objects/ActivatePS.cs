using UnityEngine;

public class ActivatePS : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;

    public void ActivateParticleSystem()
    {
        particleSystem.Play();
    }
}
