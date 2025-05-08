using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] public float WallHealth = 500;

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
