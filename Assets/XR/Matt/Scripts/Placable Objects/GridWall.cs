using UnityEngine;
using UnityEngine.UI;

public class GridWall : MonoBehaviour
{
    public int Prize;
    public int Level;

    [SerializeField] private float health = 100;
    private float maxHealth = 100;


    [SerializeField] private Image bar;


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

    private void Update()
    {
        bar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}