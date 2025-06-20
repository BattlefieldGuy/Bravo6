using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public LayerMask Mask;

    public float Damage;

    public int Level;
    public int Prize;

    [SerializeField] private float towerHealth;
    private float maxTowerHealth;

    [SerializeField] private GameObject muzzelLocation;
    [SerializeField] private BalistaModel ballistaModel;

    [SerializeField] private GameObject projectilePrefab;

    [Header("Targeting")]
    [SerializeField] float range;
    [SerializeField] float minimumRange;
    [SerializeField] float targetOffset;

    [Header("Shooting")]
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float cooldownT = 0f;

    [Header("audio")]
    [SerializeField] private AudioClip shotClip1;
    [SerializeField] private AudioClip shotClip2;
    [SerializeField] private AudioClip shotClip3;

    [SerializeField] private AudioClip destroyClip;

    private AudioSource audiosrc;

    [Header("Animations")]
    [SerializeField] private AnimationClip animClip;

    private Animation anim;

    [SerializeField] private Image bar;

    private Transform targetPosition;


    private void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animation>();
        maxTowerHealth = towerHealth;
    }

    public void TakeDamage(float _damageT)
    {
        towerHealth -= _damageT;
        if (CheckHealt())
        {
            anim.clip = animClip;
            anim.Play();

            CoinManager.GainTowerPrize(Level, Prize);

            this.GetComponent<CellManager>().RemoveItem();
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<AudioSource>().PlayOneShot(destroyClip);
            StartCoroutine(enumerator());

        }
    }

    void Update()
    {
        if (bar != null)
            bar.fillAmount = Mathf.Clamp(towerHealth / maxTowerHealth, 0, 1);

        cooldownT -= Time.deltaTime;

        Collider[] _enemiesInRange = Physics.OverlapSphere(transform.position, range, Mask);

        Transform _nearestEnemy = GetEnemy(_enemiesInRange);

        if (_nearestEnemy != null)
        {
            AimAt(_nearestEnemy);
            if (cooldownT <= 0f)
            {
                Attack(_nearestEnemy);
                cooldownT = attackCooldown;
            }
        }

    }
    void AimAt(Transform _target)
    {
        if (ballistaModel != null)
        {
            ballistaModel.AimAt(_target, muzzelLocation);

        }
        else if (ballistaModel == null)
        {
            Vector3 _direction = _target.position - muzzelLocation.transform.position;
            _direction.y = 0f;
            if (_direction.sqrMagnitude > 0.001f)
            {
                Quaternion _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * 5);
            }
        }
    }

    void Attack(Transform _target)
    {
        targetPosition = _target;
        GameObject _proj = Instantiate(projectilePrefab, muzzelLocation.transform.position, muzzelLocation.transform.rotation);
        audiosrc.PlayOneShot(ReturnShotClip());
        Projectile _projectile = _proj.GetComponent<Projectile>();
        _projectile.SetTarget(_target);
        _projectile.Damage = Damage;
    }

    Transform GetEnemy(Collider[] _enemies)
    {
        float _minDistance = Mathf.Infinity;
        Transform _nearest = null;

        foreach (var _enemy in _enemies)
        {
            float _dist = Vector3.Distance(transform.position, _enemy.transform.position);
            if (_dist < _minDistance && _dist > minimumRange)
            {
                _minDistance = _dist;
                _nearest = _enemy.transform;

            }
        }
        return _nearest;
    }

    bool CheckHealt()
    {
        if (towerHealth <= 0)
            return true;
        else return false;
    }

    AudioClip ReturnShotClip()
    {
        int _int = Random.Range(1, 3);
        return _int switch
        {
            1 => shotClip1,
            2 => shotClip2,
            3 => shotClip3,
            _ => shotClip1,
        };
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


    //Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, minimumRange);
        //Gizmos.DrawSphere(targetPosition.position, 0.1f);
    }
}