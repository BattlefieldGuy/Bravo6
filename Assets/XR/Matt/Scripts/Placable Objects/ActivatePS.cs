using UnityEngine;

public class ActivatePS : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystemSpawn;
    [SerializeField] private ParticleSystem particleSystemDestroy;

    public void ActivateSpawnPSEffect()
    {
        if (particleSystemSpawn != null)
            particleSystemSpawn.Play();
    }

    public void ActivateDestroyPSEffect()
    {
        if (particleSystemDestroy != null)
            particleSystemDestroy.Play();
    }
}
