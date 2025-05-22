using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private Wall wHealth;

    public MinionScriptableObject MSpeedData;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    RaycastHit hit;

    private void Start()
    {
        wHealth = FindFirstObjectByType<Wall>();
    }
    void Update()
    {
        Walking();
        if (CompareTag("Range"))
        {
            RangeMinion();
        }
    }

    private void Walking()
    {
        if (wHealth != null)
        {
            if (wHealth.WallHealth >= 0f)
            {
                transform.position += Vector3.forward * Time.deltaTime * MSpeedData.MSpeed;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, heart.transform.position, Time.deltaTime * MSpeedData.MSpeed);
        }
    }
    //voor range nu collider langer gemaakt, wat in theorie werkt maar idk of handig

    private void RangeMinion()
    {
        Collider[] _targets = Physics.OverlapSphere(transform.position, radius, layerMask);

        Debug.Log(hit.collider.gameObject);
        //stop met lopen en schie
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
