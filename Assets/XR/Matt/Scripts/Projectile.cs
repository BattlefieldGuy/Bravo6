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
            Destroy(gameObject);
            return;
        }

        Vector3 _direction = (target.position - transform.position).normalized;
        transform.position += _direction * Speed * Time.deltaTime;

        float _distance = Vector3.Distance(transform.position, target.position);
        if (_distance < 0.1f)
        {
            if (target.GetComponent<LayerMask>() == 6)
            {
                MinionHealth _enemy = target.GetComponent<MinionHealth>();
                if (_enemy != null)
                {
                    _enemy.TakeDamage(Damage);
                    Destroy(gameObject);
                }
            }
            else if (target.GetComponent<LayerMask>() == 7)
            {
                if (target.CompareTag("GridWall"))
                {
                    GridWall _target = target.GetComponent<GridWall>();
                    if (_target != null)
                    {
                        _target.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                }
                else if (target.CompareTag("GridTower"))
                {
                    Tower _target = target.GetComponent<Tower>();
                    if (_target != null)
                    {
                        _target.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}