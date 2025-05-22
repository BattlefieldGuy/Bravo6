using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    private float coolDown = 2;
    private float reload = 2;
    public MinionScriptableObject MDamageData;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    RaycastHit hit;

    [SerializeField] GameObject projectilePrefab;
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (CompareTag("Range"))
        {
            RangeMinion();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Damageable"))
        {
            if (coolDown <= 0)
            {
                Debug.Log("hit!");
                collision.gameObject.GetComponent<Wall>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("Heart"))
        {
            if (coolDown <= 0)
            {
                Debug.Log("POOF");
                collision.gameObject.GetComponent<Heart>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("GridTower"))
        {
            if (coolDown <= 0)
            {
                Debug.Log("boing");
                collision.gameObject.GetComponent<Tower>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("GridWall"))
        {
            if (coolDown <= 0)
            {
                Debug.Log("boing");
                collision.gameObject.GetComponent<GridWall>().TakeDamage(MDamageData.MDamage);
                coolDown = 2;
            }
        }
    }

    private void RangeMinion()
    {
        Collider[] _targets = Physics.OverlapSphere(transform.position, radius, layerMask);

        Transform _nearestTarget = GetTarget(_targets);

        if (_nearestTarget != null)
        {
            if (coolDown <= 0)
            {
                Attacking(_nearestTarget);
                coolDown = reload;
            }
        }
    }

    private Transform GetTarget(Collider[] _targets)
    {
        float _minDistance = Mathf.Infinity;
        Transform _nearest = null;

        foreach (var _target in _targets)
        {
            float _dist = Vector3.Distance(transform.position, _target.transform.position);
            if (_dist < _minDistance)
            {
                _minDistance = _dist;
                _nearest = _target.transform;
            }
        }
        return _nearest;
    }

    private void Attacking(Transform _targets)
    {
        GameObject _projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile _projectileScript = _projectile.GetComponent<Projectile>();

        _projectileScript.SetTarget(_targets);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
