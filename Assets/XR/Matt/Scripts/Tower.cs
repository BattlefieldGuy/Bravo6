using UnityEngine;

public class Tower : MonoBehaviour
{
    public LayerMask Mask;

    public float Dammage = 5;

    public int Level;
    public int Prize;

    [SerializeField] private float towerHealt = 100f;

    [SerializeField] private GameObject muzzelLocation;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] float range = 5;
    [SerializeField] float targetOffset;

    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float cooldownT = 0f;

    [Header("audio")]
    [SerializeField] private AudioClip shotClip1;
    [SerializeField] private AudioClip shotClip2;
    [SerializeField] private AudioClip shotClip3;

    private Transform targetPosition;

    private AudioSource audiosrc;

    private void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    public void TakeDamage(float _damageT)
    {
        towerHealt -= _damageT;
        if (CheckHealt())
        {
            CoinManager.GainTowerPrize(Level, Prize);
            this.GetComponent<CellManager>().DestroyItem();
        }
    }

    void Update()
    {
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
        Vector3 _direction = _target.position - muzzelLocation.transform.position;
        _direction.y = 0f;
        if (_direction.sqrMagnitude > 0.001f)
        {
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * 5);
        }
    }

    void Attack(Transform _target)
    {
        targetPosition = _target;
        GameObject _proj = Instantiate(projectilePrefab, muzzelLocation.transform.position, muzzelLocation.transform.rotation);
        audiosrc.PlayOneShot(ReturnShotClip());
        Projectile _projectile = _proj.GetComponent<Projectile>();
        _projectile.SetTarget(_target);
    }

    Transform GetEnemy(Collider[] _enemies)
    {
        float _minDistance = Mathf.Infinity;
        Transform _nearest = null;

        foreach (var _enemy in _enemies)
        {
            float _dist = Vector3.Distance(transform.position, _enemy.transform.position);
            if (_dist < _minDistance)
            {
                _minDistance = _dist;
                _nearest = _enemy.transform;

            }
        }
        return _nearest;
    }

    bool CheckHealt()
    {
        if (towerHealt <= 0)
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


    //Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        //Gizmos.DrawSphere(targetPosition.position, 0.1f);
    }
}