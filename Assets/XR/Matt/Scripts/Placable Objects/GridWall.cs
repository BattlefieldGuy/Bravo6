using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridWall : MonoBehaviour
{
    public int Prize;
    public int Level;

    [SerializeField] private float health;
    private float maxHealth;

    [SerializeField] private Image bar;

    [SerializeField] private AudioClip wallDestroy;

    [SerializeField] private AnimationClip destroyClip;

    private Animation anim;

    private void Start()
    {
        maxHealth = health;
        anim = GetComponentInChildren<Animation>();
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            anim.clip = destroyClip;
            anim.Play();
            CoinManager.GainTowerPrize(Level, Prize);
            this.GetComponent<CellManager>().RemoveItemR();
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<AudioSource>().PlayOneShot(wallDestroy);
            StartCoroutine(enumerator());
        }
    }

    private void Update()
    {
        bar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}