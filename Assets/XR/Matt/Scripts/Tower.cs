using UnityEngine;

public class Tower : MonoBehaviour
{
    public LayerMask Mask;

    public float Dammage = 5;

    [SerializeField] float range = 5;

    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float cooldownT = 0f;

    private float towerHealt = 100f;

    void Update()
    {
        cooldownT -= Time.deltaTime;

        Collider[] _enemiesInRange = Physics.OverlapSphere(transform.position, range, Mask);

        Transform _nearestEnemy = GetEnemy(_enemiesInRange);

        if (_nearestEnemy != null && cooldownT <= 0f)
        {
            Attack(_nearestEnemy);
            cooldownT = attackCooldown;
        }
    }

    public void TakeDamage(float _damageT)
    {
        towerHealt -= _damageT;
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

    void Attack(Transform _target)
    {
        Debug.Log("Attack: " + _target.name);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _target.transform.position, out hit))
        {
            //spawn particle
            //do damage
            hit.collider.GetComponent<MinionHealth>().TakeDamage(Dammage);
            Debug.Log("Do damage to: " + hit.collider.name);

        }
    }

    //Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}