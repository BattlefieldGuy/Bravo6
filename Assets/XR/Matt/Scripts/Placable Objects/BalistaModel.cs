using UnityEngine;

public class BalistaModel : MonoBehaviour
{
    public void AimAt(Transform _target, GameObject _muzzelLocation)
    {
        Vector3 _direction = _target.position - _muzzelLocation.transform.position;
        _direction.y = 0f;
        if (_direction.sqrMagnitude > 0.001f)
        {
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * 5);
        }
    }
}