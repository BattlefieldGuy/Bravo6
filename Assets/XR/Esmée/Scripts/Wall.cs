using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject wallMesh;
    [SerializeField] private AudioSource deathAuio;
    [SerializeField] private Image bar;
    [SerializeField] public float WallHealth = 500;
    private float maxHealth;


    private void Start()
    {
        maxHealth = WallHealth;
    }
    private void Update()
    {
        if (bar != null)
        {
            bar.fillAmount = Mathf.Clamp(WallHealth / maxHealth, 0, 1);
        }
    }
    private void BreakWall()
    {
        if (WallHealth <= 0)
        {
            deathAuio.Play();
            GetComponent<BoxCollider>().enabled = false;
            Destroy(wallMesh); //hier moet dan eigenlijk een animatie van breken of een opvulling met particles
            StartCoroutine(WaitASec());
        }
    }

    public void TakeDamage(float _damageW)
    {
        WallHealth -= _damageW;
        Debug.Log("waaaa dmage");
        if (CheckHealth()) BreakWall();
    }

    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public bool CheckHealth()
    {
        if (WallHealth <= 0)
        {
            Debug.LogError("DEAD");
            return true;
        }
        else
            return false;
    }
}
