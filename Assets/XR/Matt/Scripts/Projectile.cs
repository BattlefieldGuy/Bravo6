using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public float Damage = 10f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Debug.Log("Target = null");
            Destroy(gameObject);
            return;
        }

        Vector3 _direction = (target.position - transform.position).normalized;
        transform.position += _direction * Speed * Time.deltaTime;

        float _distance = Vector3.Distance(transform.position, target.position);
        if (_distance < 0.1f)
        {
            MinionHealth _enemy = target.GetComponent<MinionHealth>();
            if (_enemy != null)
            {
                _enemy.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}