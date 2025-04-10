using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothing = 1f;

    private Vector3 _offset;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _offset = _transform.position - _target.position;
        _offset = Vector3.forward * _offset.z;
    }

    private void LateUpdate()
    {
        Vector3 nextPosition = Vector3.Lerp(_transform.position, _target.position + _offset, _smoothing * Time.fixedDeltaTime);
        _transform.position = nextPosition;
    }
}