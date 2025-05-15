using UnityEngine;

public class GridWall : MonoBehaviour
{
    [SerializeField] private float health = 100;

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            this.GetComponent<CellManager>().DestroyItem();
        }
    }
}