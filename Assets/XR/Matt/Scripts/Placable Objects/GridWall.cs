using UnityEngine;

public class GridWall : MonoBehaviour
{
    public int Prize;
    public int Level;

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