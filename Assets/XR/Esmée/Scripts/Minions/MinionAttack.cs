using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    public MinionScriptableObject MDamageData;
    public MinionScriptableObject MAttackAudio;

    private float coolDown = 2;
    private float reload = 2;

    private AudioSource audioSource;

    private Animator animator;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] GameObject projectilePrefab;

    [SerializeField] GameObject lWall;
    [SerializeField] GameObject rWall;


    MinionMovement movement;


    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = FindFirstObjectByType<MinionMovement>();
        audioSource = GetComponent<AudioSource>();
    }
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
                collision.gameObject.GetComponent<Wall>().TakeDamage(MDamageData.MDamage);
                PlayAudio();
                if (animator != null) animator.SetTrigger("Attack");
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("Heart"))
        {
            if (coolDown <= 0)
            {
                collision.gameObject.GetComponent<Heart>().TakeDamage(MDamageData.MDamage);
                PlayAudio();
                if (animator != null) animator.SetTrigger("Attack");
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("GridTower"))
        {
            if (coolDown <= 0)
            {
                collision.gameObject.GetComponent<Tower>().TakeDamage(MDamageData.MDamage);
                PlayAudio();
                if (animator != null) animator.SetTrigger("Attack");
                coolDown = 2;
            }
        }

        if (collision.collider.CompareTag("GridWall"))
        {
            if (coolDown <= 0)
            {
                collision.gameObject.GetComponent<GridWall>().TakeDamage(MDamageData.MDamage);
                PlayAudio();
                if (animator != null) animator.SetTrigger("Attack");
                coolDown = 2;
            }
        }

        if (gameObject.layer == LayerMask.NameToLayer("enemyL"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("enemyR"))
            {
                movement.IsWalking = false;

                if (coolDown <= 0)
                {
                    collision.gameObject.GetComponent<MinionHealth>().TakeDamage(MDamageData.MDamage);
                    PlayAudio();
                    if (animator != null) animator.SetTrigger("Attack");
                    coolDown = 2;
                }
            }
            else
            {
                movement.IsWalking = true;
            }
        }


        if (gameObject.layer == LayerMask.NameToLayer("enemyR"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("enemyL"))
            {
                movement.IsWalking = false;

                if (coolDown <= 0)
                {
                    collision.gameObject.GetComponent<MinionHealth>().TakeDamage(MDamageData.MDamage);
                    PlayAudio();
                    if (animator != null) animator.SetTrigger("Attack");
                    coolDown = 2;
                }
            }
            else
            {
                movement.IsWalking = true;
            }
        }
    }

    private void RangeMinion()
    {
        if (gameObject.layer == LayerMask.NameToLayer("enemyL"))
        {
            Collider[] _targets = Physics.OverlapSphere(transform.position, radius, layerMask);

            Transform _nearestTarget = GetTarget(_targets);

            if (_nearestTarget != null && rWall.layer == LayerMask.NameToLayer("TargetsR"))
            {
                movement.IsWalking = false;

                if (coolDown <= 0)
                {
                    PlayAudio();
                    Attacking(_nearestTarget);
                    coolDown = reload;
                }

            }
            else if (_nearestTarget == null)
            {
                movement.IsWalking = true;
            }
        }


        else if (gameObject.layer == LayerMask.NameToLayer("enemyR")) //uhhhh
        {
            Collider[] _targets = Physics.OverlapSphere(transform.position, radius, layerMask);

            Transform _nearestTarget = GetTarget(_targets);

            if (_nearestTarget != null && lWall.layer == LayerMask.NameToLayer("TargetsL"))
            {
                movement.IsWalking = false;

                if (coolDown <= 0)
                {
                    PlayAudio();
                    Attacking(_nearestTarget);
                    coolDown = reload;
                }

            }
            else if (_nearestTarget == null)
            {
                movement.IsWalking = true;
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
        _projectileScript.Damage = MDamageData.MDamage;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public void PlayAudio()
    {
        if (MAttackAudio != null && MAttackAudio.MAttackAudio != null)
        {
            audioSource.clip = MAttackAudio.MAttackAudio;
            audioSource.Play();
        }
    }
}

