using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private CowCatcher _cowCatcher;
    [SerializeField] private float _constantForcePower;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private ConstantForce _constantForce;

    private void Awake()
    {
        _playerInput = gameObject.AddComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _engine.Initialize(_rigidbody, _playerInput);
        _cowCatcher.SetInput(_playerInput);
        _constantForce = GetComponent<ConstantForce>();
    }

    private void OnEnable()
    {
        _playerInput.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
    }

    private void OnDisable()
    {
        _playerInput.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
    }

    private void OnHorizontalDirectionChanged(float direction)
    {
        _constantForce.force = -Vector3.right * direction * _constantForcePower;
    }
}