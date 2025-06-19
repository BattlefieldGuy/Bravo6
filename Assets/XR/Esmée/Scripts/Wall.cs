using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] public float WallHealth = 500;
    private float maxHealth;

    private void Start()
    {
        maxHealth = WallHealth;
    }
    private void Update()
    {
        bar.fillAmount = Mathf.Clamp(WallHealth / maxHealth, 0, 1);
    }
    private void BreakWall()
    {
        if (WallHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float _damageW)
    {
        WallHealth -= _damageW;
        Debug.Log("waaaa dmage");
        BreakWall();
    }
}
