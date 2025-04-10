using UnityEngine;

public class Engine : MonoBehaviour
{
    private const float AltitudeOffset = 1f;

    [Header("Spherecast")]
    [SerializeField] private float _spherecastRadius;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    [Header("Lift")]
    [SerializeField] private float _force;
    [SerializeField] private float _damping;

    private Rigidbody _targetBody;
    private Transform _transform;
    private PlayerInput _playerInput;
    private float _distance;
    private float _direction;
    private float _forceFactor;
    private float _springSpeed;
    private float _previousDistance;
    private float _altitude;
    private bool _isOverrided;
    
    private Vector3 Forward 
    {
        get 
        {
            if(_transform != null)
                return _transform.forward;
            else
                return transform.forward;
        } 
    }
    private float MinAltitude => _altitude - AltitudeOffset;
    private float MaxAltitude => _altitude + AltitudeOffset;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_targetBody == null)
            return;

        if (_isOverrided)
            ForceUpDown();
        else
            Lift();
       
        ApplySpringSpeed();
    }

    private void OnDisable()
    {
        if (_playerInput != null)
            _playerInput.VerticalDirectionChanged -= OnVerticalDirectionChanged;
    }

    public void Initialize(Rigidbody rigidbody, PlayerInput playerInput)
    {
        _targetBody = rigidbody;
        _transform = transform;
        _playerInput = playerInput;
        _playerInput.VerticalDirectionChanged += OnVerticalDirectionChanged;
    }

    private float GetCurrentAltitude()
    {
        if (IsSpherecastHitGround(out RaycastHit hitInfo))
            return hitInfo.distance;

        return _maxDistance;
    }

    private void ForceUpDown()
    {
        _forceFactor = (_direction > 0) ? 1 : 0;

        if(_transform.position.y < _maxDistance)
        {
           _targetBody.AddForce(-Forward * Mathf.Max(_forceFactor * _force - _springSpeed * _force * _damping, 0), ForceMode.Force);
        }
    }

    private void Lift()
    {
        if (IsSpherecastHitGround(out RaycastHit hitInfo))
        {
            _distance = hitInfo.distance;
            _forceFactor = Mathf.Clamp(_distance, MinAltitude, MaxAltitude).Remap(MinAltitude, MaxAltitude, 1, 0);
            _targetBody.AddForce(-Forward * Mathf.Max(_forceFactor * _force - _springSpeed * _force * _damping, 0), ForceMode.Force);
        }
    }

    private void ApplySpringSpeed()
    {
        _springSpeed = (_distance - _previousDistance) * Time.fixedDeltaTime;
        _springSpeed = Mathf.Max(_springSpeed, 0);
        _previousDistance = _distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + transform.forward * _maxDistance;

        Gizmos.DrawWireCube(startPoint, Vector3.one * 0.2f);
        

        if(Physics.SphereCast(transform.position, _spherecastRadius, Forward, out RaycastHit hitInfo,
            _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawSphere(hitInfo.point, _spherecastRadius);
            Gizmos.DrawLine(startPoint, hitInfo.point);
        }
    }

    private void OnVerticalDirectionChanged(float direction)
    {
        _direction = direction;

        if (Mathf.Approximately(direction, 0) == false)
        {
            _altitude = GetCurrentAltitude();
            _altitude = Mathf.Clamp(_altitude, _spherecastRadius, _maxDistance);
            _isOverrided = true;
        }
        else
        {
            _isOverrided = false;
        }
    }

    private bool IsSpherecastHitGround(out RaycastHit hitInfo)
    {
        return Physics.SphereCast(_transform.position, _spherecastRadius, Forward, out hitInfo,
            _maxDistance, _layerMask, QueryTriggerInteraction.Ignore);
    }
}