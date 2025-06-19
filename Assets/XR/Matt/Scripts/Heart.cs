using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Image bar;
    private float health = 100;
    private float maxHealth;


    private void Start()
    {
        maxHealth = health;
    }

    private void Update()
    {
        bar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if (CheckHealth())
            Destroy(gameObject);
    }

    public bool CheckHealth()
    {
        if (health < 0.0001)
        {
            Debug.LogError("DEAD");
            return true;
        }
        else
            return false;
    }
}