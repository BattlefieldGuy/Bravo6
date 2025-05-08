using UnityEngine;

public class Tower : MonoBehaviour
{
    public LayerMask Mask;

    public float Dammage = 5;

    [SerializeField] private GameObject muzzelLocation;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] float range = 5;
    [SerializeField] float targetOffset;

    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float cooldownT = 0f;

    private Transform targetPosition = null;

    private float towerHealt = 100f;

    public void TakeDamage(float _damageT)
    {
        towerHealt -= _damageT;
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
            muzzelLocation.transform.rotation = Quaternion.Lerp(muzzelLocation.transform.rotation, _lookRotation, Time.deltaTime * 5);
        }
    }

    void Attack(Transform _target)
    {
        Debug.Log("Attack: " + _target.name);
        targetPosition = _target;
        GameObject _proj = Instantiate(projectilePrefab, muzzelLocation.transform.position, muzzelLocation.transform.rotation);
        Projectile prokectile = _proj.GetComponent<Projectile>();
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

    //Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawSphere(targetPosition.position, 0.1f);
    }
}