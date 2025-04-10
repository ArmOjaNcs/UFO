using UnityEngine;

public class AntiRoll : MonoBehaviour
{
    [SerializeField] private float _stabilizerForce;
    [SerializeField] private float _damping;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _lastDot;

    private Vector3 Up => _transform.up;
    private float Dot => Vector3.Dot(Up, Vector3.up);
    private float DotDifference => (_lastDot - Dot) * Time.fixedDeltaTime;
    private Vector3 Axis => Vector3.Cross(Up, Vector3.up);
    private Vector3 ProjectableVector => Vector3.ProjectOnPlane(Up, Vector3.up).normalized;
    private Quaternion AntiRolloverRotation => Quaternion.FromToRotation(Up, ProjectableVector);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void FixedUpdate()
    {
        Stabilize();
        ApplyDamping();
        UseAntiRolloverForces();
    }

    private void Stabilize()
    {
        if (Dot > 0)
            _rigidbody.AddTorque(Axis * (1 - Dot) * _stabilizerForce, ForceMode.Force);
    }

    private void ApplyDamping()
    {
        if(DotDifference > 0)
            _rigidbody.AddTorque(-Axis * DotDifference * _stabilizerForce, ForceMode.Force);
    }

    private void UseAntiRolloverForces()
    {
        if(Dot <  0 || Mathf.Approximately(Dot, 0))
            _transform.rotation *= AntiRolloverRotation;
    }
}